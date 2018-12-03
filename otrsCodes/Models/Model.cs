namespace otrsCodes.Models
{
    using System.Data.Entity;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model3")
        {
        }

        public virtual DbSet<Codes> Codes { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Networks> Networks { get; set; }
        public virtual DbSet<Zones> Zones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Codes>()
                .Property(e => e.Code)
                .IsFixedLength();

            modelBuilder.Entity<Colors>()
                .Property(e => e.Hex)
                .IsFixedLength();

            modelBuilder.Entity<Colors>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Colors)
                .HasForeignKey(e => e.ColorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Countries>()
                .Property(e => e.Code)
                .IsFixedLength();

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Codes)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.CountryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.CountryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Zones)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.CountryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Networks>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Networks>()
                .HasMany(e => e.Codes)
                .WithRequired(e => e.Networks)
                .HasForeignKey(e => e.NetworkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zones>()
                .HasMany(e => e.Codes)
                .WithRequired(e => e.Zones)
                .HasForeignKey(e => e.ZoneId)
                .WillCascadeOnDelete(false);
        }
    }
}
