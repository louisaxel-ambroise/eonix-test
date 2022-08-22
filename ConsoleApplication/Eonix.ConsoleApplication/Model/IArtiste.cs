namespace Eonix.ConsoleApplication.Model;

public interface IArtiste
{
    string Nom { get; }
    event EventHandler<PerformanceEvent> Reactions;
}
