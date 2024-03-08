using Microsoft.AspNetCore.Builder;

namespace ProjetRec.Models
{
    public class Candidat
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;

        // [EmailAddress]
        public string Email { get; set; } = null!;
        public string Tel { get; set; } = null!;
        // [Required]
        // [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string Skills { get; set; } = null!;
        public string Resume { get; set; } = null!;

        public ICollection<Application>? Application { get; set; }
    }
}
