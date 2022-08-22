using Eonix.Api.Model;
using Eonix.Api.Utils;

namespace Eonix.Api.Requests
{
    public class PersonneFilter : IFilter<Personne>
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public IQueryable<Personne> ApplyTo(IQueryable<Personne> query)
        {
            if (!string.IsNullOrWhiteSpace(Prenom))
            {
                query = query.Where(x => x.Prenom.ToLower().StartsWith(Prenom.ToLower()) || x.Prenom.ToLower().EndsWith(Prenom.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(Nom))
            {
                query = query.Where(x => x.Nom.ToLower().StartsWith(Nom.ToLower()) || x.Nom.ToLower().EndsWith(Nom.ToLower()));
            }

            return query;
        }
    }
}
