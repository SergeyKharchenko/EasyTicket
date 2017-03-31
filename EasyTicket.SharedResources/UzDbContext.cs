using System.Data.Entity;
using EasyTicket.SharedResources.Models.Tables;

namespace EasyTicket.SharedResources {
    public class UzDbContext : DbContext {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public UzDbContext(string connectionString = "UzConnectionString") : base(connectionString) {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UzDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>().HasRequired(reservation => reservation.Request);
        }
    }
}