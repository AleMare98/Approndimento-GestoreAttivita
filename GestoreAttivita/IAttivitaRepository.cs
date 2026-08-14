namespace GestoreAttivita;

public interface IAttivitaRepository
{ 
    // non ci vuole string percorsoFile perché l'interfaccia non conosce come verrà usato
    Task<List<Attivita>> CaricaAsync( CancellationToken cancellationToken);
    Task SalvaAsync(IReadOnlyList<Attivita> attivita, CancellationToken cancellationToken);
}