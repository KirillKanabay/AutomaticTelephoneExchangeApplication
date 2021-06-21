using System.Reflection;
using ATE.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ATE.Infrastructure.Data
{
    public class AteContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Company> Companies { get; set; }

        public AteContext()
        {
            
        }
        
        public AteContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=ate_db;");
        }
    }
}
