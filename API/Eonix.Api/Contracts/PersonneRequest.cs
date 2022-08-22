using System.ComponentModel.DataAnnotations;

namespace Eonix.Api.Contracts
{
    public class CreatePersonneRequest
    {
        [Required, MaxLength(100)]
        public string Prenom { get; set; }

        [Required, MaxLength(100)]
        public string Nom { get; set; }
    }

    public class UpdatePersonneRequest
    {
        [Required, MaxLength(100)]
        public string Prenom { get; set; }

        [Required, MaxLength(100)]
        public string Nom { get; set; }
    }
}
