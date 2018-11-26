namespace otrsCodes.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Codes
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public int NetworkId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public virtual Networks Networks { get; set; }

        public virtual Countries Countries { get; set; }
    }
}