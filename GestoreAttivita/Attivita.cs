namespace GestoreAttivita;

public class Attivita
{
    public string Titolo { get; private set; }
    public string? Descrizione { get; private set; }
    public DateOnly Scadenza { get; private set; }
    public StatoAttivita Stato { get; private set; }
    public Priorita Priorita { get; private set; }

    public Attivita(
        string titolo,
        string? descrizione,
        DateOnly scadenza,
        StatoAttivita stato,
        Priorita priorita)
    {
        if (string.IsNullOrWhiteSpace(titolo))
        {
            throw new AttivitaNonValidaException(
                "Il titolo è obbligatorio.",
                nameof(titolo));
        }

        if (scadenza < DateOnly.FromDateTime(DateTime.Today))
        {
            throw new AttivitaNonValidaException(
                "La scadenza non può essere nel passato.",
                nameof(scadenza));
        }

        if (!Enum.IsDefined(stato))
        {
            throw new AttivitaNonValidaException(
                "Lo stato non è valido.",
                nameof(stato));
        }
        
        if (!Enum.IsDefined(priorita))
        {
            throw new AttivitaNonValidaException(
                "La priorità non è valida.",
                nameof(priorita));
        }

    

        Titolo = titolo;
        Descrizione = descrizione;
        Scadenza = scadenza;
        Stato = stato;
        Priorita = priorita;
    }
}
