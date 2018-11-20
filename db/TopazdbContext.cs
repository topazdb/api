using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static api.db.DatabaseCredentialProvider;

namespace api.db {
    public partial class TopazdbContext : DbContext {
        public TopazdbContext() {}

        public TopazdbContext(DbContextOptions<TopazdbContext> options) : base(options) {}

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Instruments> Instruments { get; set; }
        public virtual DbSet<InstrumentTypes> InstrumentTypes { get; set; }
        public virtual DbSet<Lands> Lands { get; set; }
        public virtual DbSet<Scans> Scans { get; set; }
        public virtual DbSet<Sets> Sets { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseMySQL("server="+hostname+";port=3306;user="+user+";password="+password+";database="+database);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Authors>(entity => {

                entity.ToTable("authors", "topazdb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Contact)
                    .HasColumnName("contact")
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Instruments>(entity => {

                entity.ToTable("instruments", "topazdb");

                entity.HasIndex(e => new { e.InstrumentTypeId, e.SerialNo })
                    .HasName("instruments_typeId_serialNo_un")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CalibrationDate)
                    .HasColumnName("calibrationDate")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.InstrumentTypeId)
                    .HasColumnName("instrumentTypeId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.SerialNo)
                    .HasColumnName("serialNo")
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.HasOne(d => d.InstrumentType)
                    .WithMany(p => p.Instruments)
                    .HasForeignKey(d => d.InstrumentTypeId)
                    .HasConstraintName("instruments_typeId_fk");
            });

            modelBuilder.Entity<InstrumentTypes>(entity => {

                entity.ToTable("instrumentTypes", "topazdb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasColumnName("manufacturer")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnName("version")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lands>(entity => {

                entity.ToTable("lands", "topazdb");

                entity.HasIndex(e => e.ScanId)
                    .HasName("lands_scanId_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.ScanId)
                    .HasColumnName("scanId")
                    .HasColumnType("bigint(20) unsigned");

                entity.HasOne(d => d.Scan)
                    .WithMany(p => p.Lands)
                    .HasForeignKey(d => d.ScanId)
                    .HasConstraintName("lands_scanId_fk");
            });

            modelBuilder.Entity<Scans>(entity => {

                entity.ToTable("scans", "topazdb");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("scans_authorId_fk");

                entity.HasIndex(e => e.InstrumentId)
                    .HasName("scans_instrumentId_fk");

                entity.HasIndex(e => e.SetId)
                    .HasName("scans_setId_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("authorId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BarrelNo)
                    .HasColumnName("barrelNo")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BulletNo)
                    .HasColumnName("bulletNo")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.InstrumentId)
                    .HasColumnName("instrumentId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Magnification)
                    .HasColumnName("magnification")
                    .HasColumnType("int(11) unsigned")
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.Resolution)
                    .HasColumnName("resolution")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("NULL");

                entity.Property(e => e.SetId)
                    .HasColumnName("setId")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Threshold)
                    .HasColumnName("threshold")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("NULL");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scans_authorId_fk");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scans_instrumentId_fk");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.Scans)
                    .HasForeignKey(d => d.SetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("scans_setId_fk");
            });

            modelBuilder.Entity<Sets>(entity => {

                entity.ToTable("sets", "topazdb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Settings>(entity => {

                entity.HasKey(e => e.Name);

                entity.ToTable("settings", "topazdb");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .IsUnicode(false)
                    .HasDefaultValueSql("NULL");
            });
        }
    }
}
