namespace Eonix.ConsoleApplication.Model;

public class PerformanceEvent : EventArgs
{
    public TypeDeTour Type { get; }
    public string Tour { get; }
    public string Artiste { get; }

    public PerformanceEvent(Tour tour, IArtiste artiste)
    {
        Type = tour.Type;
        Tour = tour.Nom;
        Artiste = artiste.Nom;
    }
}
