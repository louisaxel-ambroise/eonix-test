using ReactionAction = System.Action<Eonix.ConsoleApplication.Model.Spectateur, Eonix.ConsoleApplication.Model.PerformanceEvent>;

namespace Eonix.ConsoleApplication.Model;

public sealed class Spectateur
{

    public string Nom { get; }
    public IReadOnlyDictionary<TypeDeTour, ReactionAction> Reactions { get; }

    public Spectateur(string nom, IReadOnlyDictionary<TypeDeTour, ReactionAction> reactions)
    {
        ArgumentNullException.ThrowIfNull(nom);
        ArgumentNullException.ThrowIfNull(reactions);

        Nom = nom;
        Reactions = reactions;
    }

    public void Observer(IArtiste artiste)
    {
        artiste.Reactions += Reaction;
    }

    public void Quitter(IArtiste artiste)
    {
        artiste.Reactions -= Reaction;
    }

    private void Reaction(object performer, PerformanceEvent representation)
    {
        if(Reactions.TryGetValue(representation.Type, out var reaction))
        {
            reaction(this, representation);
        }
    }
}
