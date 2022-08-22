using Eonix.Api.Model;

namespace Eonix.Api.Requests
{
    public sealed class PersonneResponse
    {
        public Guid Id { get; }
        public string Prenom { get; }
        public string Nom { get; }

        public PersonneResponse(Personne personne)
        {
            Id = personne.Id;
            Prenom = personne.Prenom;
            Nom = personne.Nom;
        }
    }
}
