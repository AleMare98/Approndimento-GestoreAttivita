namespace GestoreAttivita;

public class GestoreAttivitaService
{
    
    // *** SERVICE ***
    private readonly IAttivitaRepository _repository;
    
    private readonly List<Attivita> _attivita = [];
    
    public IReadOnlyList<Attivita> Attivita => _attivita.AsReadOnly();
    
    public GestoreAttivitaService(IAttivitaRepository repository) // costruttore
    {
        _repository = repository;
    }

    public void Aggiungi(Attivita attivita)
    {
        ArgumentNullException.ThrowIfNull(attivita);
        _attivita.Add(attivita);
    }
    
    public int Conta()
    {
        return _attivita.Count;
    }
    
    
    public async Task CaricaAsync(CancellationToken cancellationToken)
    {
        var attivitaCaricate = await _repository.CaricaAsync(cancellationToken);

        _attivita.Clear();
        _attivita.AddRange(attivitaCaricate);
    }
    
    public async Task SalvaAsync(CancellationToken cancellationToken)
    {
        await _repository.SalvaAsync(Attivita, cancellationToken);
    }
}