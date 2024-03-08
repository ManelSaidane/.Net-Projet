namespace ProjetRec.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;

        //  [ForeignKey("Candidat")]
        public int CandidatId { get; set; }

        //  [ForeignKey("Job")]
        public int JobId { get; set; }

        public virtual Candidat? Candidat { get; set; }
        public virtual Job? Job { get; set; }
    }
}
