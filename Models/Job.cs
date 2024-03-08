using Microsoft.AspNetCore.Builder;

namespace ProjetRec.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SkillsNeeded { get; set; } = null!;
        public string Location { get; set; } = null!;

        //[ForeignKey("Entreprise")]
        public int EntrepriseId { get; set; }

        public virtual Entreprise? Entreprise { get; set; }

        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
