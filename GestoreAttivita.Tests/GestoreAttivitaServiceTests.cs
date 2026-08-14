using GestoreAttivita;
using Xunit;

namespace GestoreAttivita.Tests;

public class GestoreAttivitaServiceTests
{
    [Fact]
    public void Aggiungi_ConAttivitaValida_AumentaIlNumeroDiAttivita()
    {
        // Arrange
        var repository = new RepositoryFinta();
        var service = new GestoreAttivitaService(repository);

        // Crea qui un'Attivita valida:
        // titolo, descrizione, scadenza futura, stato, priorità.
        var attivita = new Attivita("bla", "bla", DateOnly.FromDateTime(DateTime.Today.AddDays(1)), StatoAttivita.DaFare, Priorita.Media);

        // Act
        // Chiama Aggiungi.
        service.Aggiungi(attivita);

        // Assert
        // Verifica con Assert.Equal che Conta() restituisca 1.
        Assert.Equal(1, service.Conta());
    }

    [Fact]
    public void Creazione_ConTitoloNonValido_VieneLanciataAttivitaNonValidaException()
    {
        Action creaAttivita = () => new Attivita(
            "",
            "bla",
            DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            StatoAttivita.DaFare,
            Priorita.Media);

        Assert.Throws<AttivitaNonValidaException>(creaAttivita);
    }

    [Fact]
    public void Aggiungi_ConTitoloDuplicato_VieneRifiutataOperazione()
    {
        var repository = new RepositoryFinta();
        var service = new GestoreAttivitaService(repository);
        
        service.Aggiungi(new Attivita("bla", "bla", DateOnly.FromDateTime(DateTime.Today.AddDays(1)), StatoAttivita.DaFare, Priorita.Media));
        
        Action aggiungiDuplicato = () => service.Aggiungi(new Attivita("bla", "bla", DateOnly.FromDateTime(DateTime.Today.AddDays(1)), StatoAttivita.DaFare, Priorita.Media));
        
        Assert.Throws<InvalidOperationException>(aggiungiDuplicato);
    }
}