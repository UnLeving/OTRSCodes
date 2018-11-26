namespace otrsCodes.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Codes
    {
        [Key]
        public int Id { get; set; }
        
        public int CountryId { get; set; }
        
        public int NetworkId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [ForeignKey("NetworkId")]
        public virtual Networks Networks { get; set; }
        [ForeignKey("CountryId")]
        public virtual Countries Countries { get; set; }
    }
}