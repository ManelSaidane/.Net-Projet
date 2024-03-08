using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProjetRec.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }

       

        public virtual DbSet<Candidat> Candidats { get; set; }
        public virtual DbSet<Entreprise> Entreprises { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CANDIDAT
            modelBuilder.Entity<Candidat>().ToTable("ACandidat");
            modelBuilder.Entity<Candidat>().HasKey(h => h.Id);
            modelBuilder.Entity<Candidat>().Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Candidat>().Property(p => p.Email)
                .IsRequired();
            modelBuilder.Entity<Candidat>().Property(p => p.Tel)
               .IsRequired()
               .HasMaxLength(8);
            modelBuilder.Entity<Candidat>().Property(p => p.Password)
              .IsRequired()
              .HasMaxLength(12);
            modelBuilder.Entity<Candidat>().Property(p => p.Skills)
             .IsRequired();
            modelBuilder.Entity<Candidat>().Property(p => p.Resume)
             .IsRequired();
            modelBuilder.Entity<Candidat>().Property(p => p.Surname)
            .IsRequired();
            modelBuilder.Entity<Candidat>()
                .HasMany(h => h.Application)
                .WithOne(a => a.Candidat)
                .HasForeignKey(resto => resto.CandidatId);

            //Entreprise
            modelBuilder.Entity<Entreprise>().ToTable("AEntreprise");
            modelBuilder.Entity<Entreprise>().HasKey(h => h.Id);

            modelBuilder.Entity<Entreprise>().Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Entreprise>().Property(p => p.Email)
               .IsRequired();
            modelBuilder.Entity<Entreprise>().Property(p => p.Tel)
              .IsRequired()
              .HasMaxLength(8);
            modelBuilder.Entity<Entreprise>().Property(p => p.Password)
             .IsRequired()
             .HasMaxLength(12);
            modelBuilder.Entity<Entreprise>().Property(p => p.Description)
              .IsRequired();
            modelBuilder.Entity<Entreprise>().Property(p => p.Location)
             .IsRequired();
            modelBuilder.Entity<Entreprise>()
               .HasMany(h => h.Job)
               .WithOne(a => a.Entreprise)
               .HasForeignKey(resto => resto.EntrepriseId);

            //JOB Job
            modelBuilder.Entity<Job>().ToTable("AJob");
            modelBuilder.Entity<Job>().HasKey(h => h.Id);
            modelBuilder.Entity<Job>().Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Job>().Property(p => p.Description)
               .IsRequired();
            modelBuilder.Entity<Job>().Property(p => p.SkillsNeeded)
             .IsRequired()
             .HasMaxLength(30);
            modelBuilder.Entity<Job>().Property(p => p.Location)
                .HasMaxLength(50)
                .IsRequired();

            //JOB APPLICATION

            modelBuilder.Entity<Application>().ToTable("AApplication");
            modelBuilder.Entity<Application>().HasKey(h => h.Id);
            modelBuilder.Entity<Application>().Property(p => p.Status)
                .IsRequired();
            modelBuilder.Entity<Application>()
               .HasOne(a => a.Job)
                .WithMany(o => o.Applications)
                .HasForeignKey(a => a.JobId);
        }
        }
}
