using Microsoft.EntityFrameworkCore;

namespace PensionAccumulationCalculator.Data {
    internal class AppDbContext : DbContext {
        public AppDbContext() : base() {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }
    }
}