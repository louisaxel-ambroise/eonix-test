using Eonix.ConsoleApplication.Model;

namespace Eonix.ConsoleApplication.Builders;

public class SpectateurBuilder
{
    private readonly string _nom;
    private readonly Dictionary<TypeDeTour, Action<Spectateur, PerformanceEvent>> _reactions = new();

    private SpectateurBuilder(string nom)
    {
        _nom = nom;
    }

    public SpectateurBuilder ReactionTourMusical(Action<Spectateur, PerformanceEvent> reaction)
    {
        _reactions.Add(TypeDeTour.Musical, reaction);

        return this;
    }

    public SpectateurBuilder ReactionTourAcrobatique(Action<Spectateur, PerformanceEvent> reaction)
    {
        _reactions.Add(TypeDeTour.Acrobatique, reaction);

        return this;
    }

    public Spectateur Creer()
    {
        if (!_reactions.Any())
        {
            throw new InvalidOperationException("Sequence contains no elements");
        }

        return new Spectateur(_nom, _reactions);
    }

    public static SpectateurBuilder Nouveau(string nom)
    {
        return new(nom);
    }
}