using Microsoft.EntityFrameworkCore;
using static db.DatabaseCredentialProvider;

namespace db {

    public class LibraryContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL("server="+hostname+";database="+database+";user="+user+";password="+password);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }   
    }
}