namespace Eonix.Api.Model
{
    public class Personne
    {
        public Guid Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }

        public Personne()
        {
        }

        public Personne(string prenom, string nom)
        {
            Prenom = prenom;
            Nom = nom;
        }
    }
}
