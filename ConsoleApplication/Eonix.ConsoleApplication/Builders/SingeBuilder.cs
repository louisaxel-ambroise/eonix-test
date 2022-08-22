using Eonix.ConsoleApplication.Model;

namespace Eonix.ConsoleApplication.Builders;

public class SingeBuilder
{
    private readonly string _nom;
    private readonly List<Tour> _tours = new();

    private SingeBuilder(string nom)
    {
        _nom = nom;
    }

    public SingeBuilder ApprendreTourMusical(string nom)
    {
        _tours.Add(new(nom, TypeDeTour.Musical));

        return this;
    }

    public SingeBuilder ApprendreTourAcrobatique(string nom)
    {
        _tours.Add(new(nom, TypeDeTour.Acrobatique));

        return this;
    }

    public Singe Creer()
    {
        if (!_tours.Any())
        {
            throw new InvalidOperationException("Sequence contains no elements");
        }

        return new Singe(_nom, _tours);
    }

    public static SingeBuilder Nouveau(string nom)
    {
        return new(nom);
    }
}
