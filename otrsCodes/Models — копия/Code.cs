namespace otrsCodes.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Code
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Value { get; set; }

        public int CountryId { get; set; }
        public int NetworkId { get; set; }
        public int ZoneId { get; set; }

        [ForeignKey("CountryId")]
        public Country Countries { get; set; }
        [ForeignKey("NetworkId")]
        public Network Networks { get; set; }
        [ForeignKey("ZoneId")]
        public Zone Zones { get; set; }
    }
}
