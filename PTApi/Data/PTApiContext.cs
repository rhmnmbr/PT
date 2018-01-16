using Microsoft.EntityFrameworkCore;
using PTApi.Data.Models;

namespace PTApi.Data
{
    public class PTApiContext : DbContext
    {
        public PTApiContext(DbContextOptions<PTApiContext> options) : base(options) { }

        public DbSet<Mahasiswa> Mahasiswa { get; set; }
        public DbSet<Kelas> Kelas { get; set; }
        public DbSet<Dosen> Dosen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MhsKls>()
                .HasKey(mk => new { mk.Nim, mk.KodeKelas });

            modelBuilder.Entity<MhsKls>()
                .HasOne(mk => mk.Mahasiswa)
                .WithMany(m => m.Klasses)
                .HasForeignKey(mk => mk.Nim);

            modelBuilder.Entity<MhsKls>()
                .HasOne(mk => mk.Kelas)
                .WithMany(k => k.Mhss)
                .HasForeignKey(mk => mk.KodeKelas);

            modelBuilder.Entity<Dosen>()
                .HasMany(d => d.Kelas)
                .WithOne(k => k.Dosen);

            // modelBuilder.Entity<Mahasiswa>()
            // .HasIndex(c => c.NIM)
            // .IsUnique();
        }
    }
}