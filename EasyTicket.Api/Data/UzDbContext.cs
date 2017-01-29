using System.Data.Entity;
using EasyTicket.Api.Data.Models;

namespace EasyTicket.Api.Data {
    public class UzDbContext : DbContext {
        public DbSet<PlaceRequest> PlaceRequests { get; set; }

        public UzDbContext(string connectionString = "UzConnectionString") : base(connectionString) {
            Database.SetInitializer(new UzDbContextInitializer());
        }

        private class UzDbContextInitializer : DropCreateDatabaseIfModelChanges<UzDbContext> {}
    }
}