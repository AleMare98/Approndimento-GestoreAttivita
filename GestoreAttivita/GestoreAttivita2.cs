using System.Text.Json;

namespace GestoreAttivita;

public class GestoreAttivita2
{
    private readonly List<Attivita> _attivita = [];

    public IReadOnlyList<Attivita> Attivita => _attivita.AsReadOnly();
    

    public void Aggiungi(Attivita attivita)
    {
        ArgumentNullException.ThrowIfNull(attivita);

        _attivita.Add(attivita);
    }

    public int Conta()
    {
        return _attivita.Count;
    }
    
    
    // public string OttieniPercorsoFile(string percorsoConfigurato)
    // {
    //     var nomeFile = "attivita.json";
    //     var percorsoFile = Path.Combine(percorsoConfigurato, nomeFile);
    //
    //     return percorsoFile;
    // }
    
    private string SerializzaAttivita(IReadOnlyList<Attivita> attivita)
    {
        var opzioniJson = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(attivita, opzioniJson);
    }
    
    // public void SalvaAttivita(List<Attivita> attivita, string percorsoFile)
    // {
    //    var path = OttieniPercorsoFile(percorsoFile);
    //    var text = SerializzaAttivita(attivita);
    //    File.WriteAllText(path, text);
    // }
    public async Task SalvaAttivitaAsync(IReadOnlyList<Attivita> attivita, string percorsoFile, CancellationToken cancellationToken)
    {
        var cartella = Path.GetDirectoryName(percorsoFile);

        if (!string.IsNullOrWhiteSpace(cartella))
        {
            Directory.CreateDirectory(cartella);
        }

        var testoJson = SerializzaAttivita(attivita);
        await File.WriteAllTextAsync(percorsoFile, testoJson, cancellationToken);
    }

    // public List<Attivita> CaricaAttivita(string percorsoFile)
    // {
    //     if (File.Exists(percorsoFile))
    //     {
    //         var f = File.ReadAllText(percorsoFile);
    //         return JsonSerializer.Deserialize<List<Attivita>>(f);
    //     }
    //     else
    //     {
    //         return null;
    //     }
    // }
    public async Task<List<Attivita>> CaricaAttivitaAsync(string percorsoFile, CancellationToken cancellationToken)
    {
        if (!File.Exists(percorsoFile))
        {
            return [];
        }

        var testoJson = await File.ReadAllTextAsync(percorsoFile, cancellationToken);

        return JsonSerializer.Deserialize<List<Attivita>>(testoJson)
               ?? [];
    }
}
