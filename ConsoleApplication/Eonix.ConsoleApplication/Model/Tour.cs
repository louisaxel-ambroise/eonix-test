namespace Eonix.ConsoleApplication.Model;

public record Tour(string Nom, TypeDeTour Type);

public enum TypeDeTour
{
    Musical,
    Acrobatique
}
