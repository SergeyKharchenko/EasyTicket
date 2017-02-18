using System;
using System.Data.Entity;
using EasyTicket.SharedResources.Models;
using EasyTicket.SharedResources.Models.Tables;

namespace EasyTicket.SharedResources {
    public class UzDbContext : DbContext {
        public DbSet<Request> Requests { get; set; }

        public UzDbContext(string connectionString = "UzConnectionString") : base(connectionString) {
            Database.SetInitializer(new UzDbContextInitializer());
        }

        private class UzDbContextInitializer : DropCreateDatabaseIfModelChanges<UzDbContext> {}
    }
}