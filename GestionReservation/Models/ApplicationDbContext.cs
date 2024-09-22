using Microsoft.EntityFrameworkCore;
using GestionReservation.Models;

namespace GestionReservation.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Secretaire> Secretaires { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Equipement> Equipements { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<SalleDeReunion> SalleDeReunions { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH Inheritance
            modelBuilder.Entity<Personne>()
                .HasDiscriminator<string>("PersonneType")
                .HasValue<Admin>("Admin")
                .HasValue<Employe>("Employe")
                .HasValue<Secretaire>("Secretaire");

            // Relations Configuration

            // Admin -> Departements (One-to-Many)
            modelBuilder.Entity<Departement>()
                .HasOne(d => d.Admin)
                .WithMany(a => a.Departements)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employe -> Departement (Many-to-One)
            modelBuilder.Entity<Employe>()
                .HasOne(e => e.Departement)
                .WithMany(d => d.Employes)
                .HasForeignKey(e => e.DepartementId)
                .OnDelete(DeleteBehavior.Restrict);

            // Secretaire -> Managed Reservations (One-to-Many)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Secretaire)
                .WithMany(s => s.ManagedReservations)
                .HasForeignKey(r => r.SecretaireId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employe -> Reservations (One-to-Many)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Employe)
                .WithMany(e => e.Reservations)
                .HasForeignKey(r => r.EmployeId)
                .OnDelete(DeleteBehavior.Restrict);

            // SalleDeReunion -> Reservations (One-to-Many)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.SalleDeReunion)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SalleDeReunionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification configuration (for Secretaire or Employe)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Secretaire)
                .WithMany(s => s.Notifications)
                .HasForeignKey(n => n.SecretaireId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Employe)
                .WithMany(e => e.Notifications)
                .HasForeignKey(n => n.EmployeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Departement>()
                .Property(d => d.Budget)
                .HasColumnType("decimal(18,2)");
        }
    }
}
