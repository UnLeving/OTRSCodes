using Microsoft.EntityFrameworkCore;

namespace otrsCode_netCore.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> contextOptions): base(contextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Code> Codes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Network> Networks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Code>()
                .Property(e => e.Value);

            modelBuilder.Entity<Code>()
                .Property(e => e.R);

            modelBuilder.Entity<Color>()
                .Property(e => e.Hex)
                .IsFixedLength();

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Networks);

            modelBuilder.Entity<Country>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Country>()
                .Property(e => e.Code);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Codes);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Networks);

            modelBuilder.Entity<Network>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Network>()
                .HasMany(e => e.Codes);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=otrs;Username=postgres;Password=qq");
        }
    }
}
