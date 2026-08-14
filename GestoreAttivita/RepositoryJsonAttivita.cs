using System.Text.Json;

namespace GestoreAttivita;

public class RepositoryJsonAttivita : IAttivitaRepository
{
    
    // *** REPOSITORY / INFRASTRUTTURA ***
    private readonly string _percorsoFile;
    
    public RepositoryJsonAttivita(string percorsoFile)
    {
        _percorsoFile = percorsoFile;
    }
    
    public async Task  SalvaAsync(IReadOnlyList<Attivita> attivita, CancellationToken cancellationToken)
    {
        var cartella = Path.GetDirectoryName(_percorsoFile);

        if (!string.IsNullOrWhiteSpace(cartella))
        {
            Directory.CreateDirectory(cartella);
        }

        var testoJson = SerializzaAttivita(attivita);
        await File.WriteAllTextAsync(_percorsoFile, testoJson, cancellationToken);
    }

    public async Task<List<Attivita>> CaricaAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_percorsoFile))
        {
            return [];
        }

        var testoJson = await File.ReadAllTextAsync(_percorsoFile, cancellationToken);

        return JsonSerializer.Deserialize<List<Attivita>>(testoJson) ?? [];
    }
    
    private string SerializzaAttivita(IReadOnlyList<Attivita> attivita)
    {
        var opzioniJson = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(attivita, opzioniJson);
    }
    
}