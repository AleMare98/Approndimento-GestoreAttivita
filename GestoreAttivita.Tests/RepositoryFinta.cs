using GestoreAttivita;

namespace GestoreAttivita.Tests;

public sealed class RepositoryFinta : IAttivitaRepository
{
    public Task<List<Attivita>> CaricaAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new List<Attivita>());
    }

    public Task SalvaAsync(IReadOnlyList<Attivita> attivita, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}