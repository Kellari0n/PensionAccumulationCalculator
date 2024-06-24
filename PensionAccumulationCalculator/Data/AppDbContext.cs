using Microsoft.EntityFrameworkCore;

using PensionAccumulationCalculator.Entities;

namespace PensionAccumulationCalculator.Data {
    internal class AppDbContext : DbContext {
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work_record> Works { get; set; }
        public DbSet<Military_record> Military_records { get; set; }
        public DbSet<Insurance_record> Insurance_records { get; set; }
        public DbSet<Individual_pencion_coefficient_accumulation> Individual_pencion_coefficient_accumulation { get; set; }
        public DbSet<Ref_coefficients_cost_by_year> Ref_coefficients_cost_by_year { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }

        public AppDbContext() : base() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}