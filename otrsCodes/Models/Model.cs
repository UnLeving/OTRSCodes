namespace otrsCodes.Models
{
    using System.Data.Entity;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Network> Networks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Code>()
                .Property(e => e.Value);

            modelBuilder.Entity<Code>()
                .Property(e => e.Zone);

            modelBuilder.Entity<Color>()
                .Property(e => e.Hex)
                .IsFixedLength();

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Color)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Country>()
                .Property(e => e.Code);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Codes)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Network>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Network>()
                .HasMany(e => e.Codes)
                .WithRequired(e => e.Network)
                .WillCascadeOnDelete(false);
        }
    }
}
