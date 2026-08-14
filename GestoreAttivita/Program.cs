//
// using GestoreAttivita;
//
//
// var gestore = new GestoreAttivita2();
//
// var attivita = CreaAttivitaDaConsole();
// gestore.Aggiungi(attivita);
//
//
// Console.WriteLine($"Attività creata: {attivita.Titolo}");
// Console.WriteLine($"Attività registrate: {gestore.Conta()}");
using GestoreAttivita;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var configurazione = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

string? percorsoArchivioAttivita =
    configurazione["ArchivioAttivita:PercorsoFile"];

if (string.IsNullOrWhiteSpace(percorsoArchivioAttivita))
{
    throw new InvalidOperationException(
        "La configurazione 'ArchivioAttivita:PercorsoFile' è assente o non valida.");
}

// var gestore = new GestoreAttivita2();
IAttivitaRepository repository = new RepositoryJsonAttivita(percorsoArchivioAttivita);
var gestore = new GestoreAttivitaService(repository);

using var annullamento = new CancellationTokenSource();


Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    annullamento.Cancel();
    Console.WriteLine("Interruzione del programma.");
};

try
{
    // var attivitaSalvate = await gestore.CaricaAttivitaAsync(percorsoArchivioAttivita, annullamento.Token);
    //
    // foreach (var attivitaSalvata in attivitaSalvate)
    // {
    //     gestore.Aggiungi(attivitaSalvata);
    // }

    await gestore.CaricaAsync(annullamento.Token);

    var attivita = CreaAttivitaDaConsole();
    annullamento.Token.ThrowIfCancellationRequested();
    gestore.Aggiungi(attivita);

    // await gestore.SalvaAttivitaAsync(gestore.Attivita, percorsoArchivioAttivita, annullamento.Token);
    await gestore.SalvaAsync(annullamento.Token);

    Console.WriteLine($"Attività creata: {attivita.Titolo}");
    Console.WriteLine($"Attività registrate: {gestore.Conta()}");

}
catch (JsonException)
{
    Console.WriteLine("Il file delle attività contiene JSON non valido.");
    return;
}

catch (OperationCanceledException)
{
    Console.WriteLine("Interruzione del programma.");
}


static DateOnly LeggiScadenza()
{
    while (true)
    {
        Console.Write("Inserisci la data di scadenza (formato gg/mm/aaaa): ");
        string? dataScadenza = Console.ReadLine();
        if (DateOnly.TryParse(dataScadenza, out var scadenza))
        {
            return scadenza;
        }
        Console.WriteLine("Data non valida. Riprova.");
    }
}

static Priorita LeggiPriorita()
{
    while (true)
    {
        Console.Write("Inserisci Bassa, Media o Alta");
        string? priorita = Console.ReadLine();

        if (Enum.TryParse<Priorita>(priorita, ignoreCase: true, out var prioritaEnum) && Enum.IsDefined(prioritaEnum))
        {
            return prioritaEnum;
        }
        Console.WriteLine("La priorita non è valida. Riprova.");
    }
}


static Attivita CreaAttivitaDaConsole()
{
    while (true)
    {
        Console.Write("inserisci il titolo:");
        string titolo = Console.ReadLine() ?? string.Empty;
        Console.Write("inserisci la descrizione opzionale:");
        string? descrizione = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(descrizione))
        {
            descrizione = null;
        }
        
        var scadenza = LeggiScadenza();
        var priorita = LeggiPriorita();

        try
        {
            var attivita = new Attivita(titolo, descrizione, scadenza, StatoAttivita.DaFare, priorita);
            return attivita;
        }
        catch(AttivitaNonValidaException e) 
        {
            Console.WriteLine($"Attività non valida: {e.Message}");
        }
    }
    

}
