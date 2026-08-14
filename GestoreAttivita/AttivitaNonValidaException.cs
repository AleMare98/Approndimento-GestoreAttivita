namespace GestoreAttivita;

public sealed class AttivitaNonValidaException : ArgumentException
{
    public AttivitaNonValidaException(string messaggio, string? nomeParametro = null)
        : base(messaggio, nomeParametro)
    {
    }
}