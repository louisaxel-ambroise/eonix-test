namespace Eonix.ConsoleApplication.Model;

public sealed class Singe : IArtiste
{
    private readonly IEnumerable<Tour> _tours;

    public string Nom { get; }
    public event EventHandler<PerformanceEvent> Reactions;

    public Singe(string nom, IEnumerable<Tour> tours)
    {
        ArgumentNullException.ThrowIfNull(nom);
        ArgumentNullException.ThrowIfNull(tours);

        Nom = nom;
        _tours = tours;
    }

    public IEnumerable<string> ToursConnus()
    {
        return _tours.Select(tour => tour.Nom);
    }

    public void Executer(string nom)
    {
        var tour = _tours.SingleOrDefault(t => t.Nom == nom);

        if(tour is null)
        {
            throw new ArgumentOutOfRangeException(string.Format("{0} ne connait pas le tour {1}", Nom, tour));
        }

        if(Reactions is not null)
        {
            Reactions(this, new (tour, this));
        }
    }
}
