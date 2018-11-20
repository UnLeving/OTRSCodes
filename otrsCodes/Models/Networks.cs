namespace otrsCodes.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Networks
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public int ColorId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual Colors Colors { get; set; }

        public virtual Countries Countries { get; set; }
    }
}
