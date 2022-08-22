namespace Eonix.ConsoleApplication.Model;

public sealed class Dresseur
{
    public string Nom { get; }
    public Singe Singe { get; }

    public Dresseur(string nom, Singe singe)
    {
        ArgumentNullException.ThrowIfNull(nom);
        ArgumentNullException.ThrowIfNull(singe);

        Nom = nom;
        Singe = singe;
    }

    public void Jouer()
    {
        foreach(var tour in Singe.ToursConnus())
        {
            Singe.Executer(tour);
        }
    }
}
