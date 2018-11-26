namespace otrsCodes.Models
{
    using System.Data.Entity;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Networks> Networks { get; set; }
        public virtual DbSet<Codes> Codes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.CountryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Networks>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Codes>()
               .Property(e => e.Code)
               .IsFixedLength();
        }
    }
}
