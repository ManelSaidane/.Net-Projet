namespace ProjetRec.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string Location { get; set; } = null!;


        public string Password { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<Job>? Job { get; set; }
    }
}
