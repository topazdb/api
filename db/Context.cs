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
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseMySQL("server="+hostname+";port=3306;user="+user+";password="+password+";database="+database);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Land>(entity => {

                entity.ToTable("lands", "topazdb");

                entity.HasIndex(e => e.scanId)
                    .HasName("lands_scanId_fk");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.path)
                    .HasColumnName("path")
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.scanId)
                    .HasColumnName("scanId")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.scan)
                    .WithMany(p => p.lands)
                    .HasForeignKey(d => d.scanId)
                    .HasConstraintName("lands_scanId_fk");
            });

            modelBuilder.Entity<Scan>(entity => {

                entity.ToTable("scans", "topazdb");

                entity.HasIndex(e => e.authorId)
                    .HasName("scans_authorId_fk");

                entity.HasIndex(e => e.instrumentId)
                    .HasName("scans_instrumentId_fk");

                entity.HasIndex(e => e.setId)
                    .HasName("scans_setId_fk");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.authorId)
                    .HasColumnName("authorId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.barrelNo)
                    .HasColumnName("barrelNo")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.bulletNo)
                    .HasColumnName("bulletNo")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.creationDate)
                    .HasColumnName("creationDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.instrumentId)
                    .HasColumnName("instrumentId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.magnification)
                    .HasColumnName("magnification")
                    .HasColumnType("int(11) unsigned")
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.resolution)
                    .HasColumnName("resolution")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.setId)
                    .HasColumnName("setId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.threshold)
                    .HasColumnName("threshold")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("NULL");

                entity.HasOne(d => d.author)
                    .WithMany(p => p.scans)
                    .HasForeignKey(d => d.authorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scans_authorId_fk");

                entity.HasOne(d => d.instrument)
                    .WithMany(p => p.scans)
                    .HasForeignKey(d => d.instrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scans_instrumentId_fk");

                entity.HasOne(d => d.set)
                    .WithMany(p => p.scans)
                    .HasForeignKey(d => d.setId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scans_setId_fk");
            });

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
