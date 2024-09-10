using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using GestionReservation.Models;

namespace GestionReservation.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Employe> Employes { get; set; }
        public DbSet<Secretaire> Secretaires { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Equipement> Equipements { get; set; }
        public DbSet<SalleDeReunion> SalleDeReunions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurer les tables pour chaque entité (facultatif)
            modelBuilder.Entity<Employe>().ToTable("Employes");
            modelBuilder.Entity<Secretaire>().ToTable("Secretaires");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Reservation>().ToTable("Reservations");
            modelBuilder.Entity<Departement>().ToTable("Departements");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Equipement>().ToTable("Equipements");
            modelBuilder.Entity<SalleDeReunion>().ToTable("SalleDeReunions");

            // Configuration spécifique pour la propriété Budget de Departement
            modelBuilder.Entity<Departement>()
                .Property(d => d.Budget)
                .HasColumnType("decimal(18, 2)");

            // Autres configurations si nécessaire...
        }
    }
}
