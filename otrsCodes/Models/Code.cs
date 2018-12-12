namespace otrsCodes.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Code")]
    public partial class Code
    {
        public int Id { get; set; }

        [Required]
        public int Value { get; set; }
        [Required]
        public int Zone { get; set; }

        public int CountryId { get; set; }
        public int NetworkId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Network Network { get; set; }
    }
}