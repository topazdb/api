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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Set>(entity => {

                entity.ToTable("sets", "topazdb");
                
                entity.Property(e => e.creationDate)
                    .HasColumnName("creationDate")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<SetView>(entity => {
                entity.ToTable("setView", "topazdb");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.creationDate)
                    .HasColumnName("creationDate")
                    .HasDefaultValueSql("current_timestamp()");
                
                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.barrelCount)
                    .HasColumnName("barrelCount");

                entity.Property(e => e.bulletCount)
                    .HasColumnName("bulletCount");
                
                entity.Property(e => e.lastScanDate)
                    .HasColumnName("lastScanDate")
                    .HasDefaultValueSql("NULL");
            });

            modelBuilder.Entity<Setting>(entity => {

                entity.HasKey(e => e.name);

                entity.ToTable("settings", "topazdb");

                entity.Property(e => e.name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.value)
                    .HasColumnName("value")
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");
            });
        }
    }
}
