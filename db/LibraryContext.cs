using Microsoft.EntityFrameworkCore;

namespace db {
    public class LibraryContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL("server=;database=;user=;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }   
    }
}