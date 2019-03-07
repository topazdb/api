using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using api.Models;

namespace api.db {
    using static CredentialProvider;

    public partial class Context : DbContext {
        public Context() {}

        public Context(DbContextOptions<Context> options) : base(options) {}

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Instrument> Instruments { get; set; }
        public virtual DbSet<InstrumentType> InstrumentTypes { get; set; }
        public virtual DbSet<Land> Lands { get; set; }
        public virtual DbSet<Scan> Scans { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<SetView> SetView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL("server="+hostname+";port=3306;user="+user+";password="+password+";database="+database);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {}
    }
}
