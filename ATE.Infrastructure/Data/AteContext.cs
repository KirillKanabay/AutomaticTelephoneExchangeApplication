using System.Reflection;
using ATE.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ATE.Infrastructure.Data
{
    public class AteContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public AteContext(DbContextOptions options):base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
