namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Network
    {
        public Network()
        {
            Codes = new HashSet<Code>();
        }
        [Key]
        public int Id { get; set; }

        public int CountryId { get; set; }

        public int ColorId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        
        public ICollection<Code> Codes { get; set; }
        [ForeignKey("ColorId")]
        public Color Colors { get; set; }
        [ForeignKey("CountryId")]
        public Country Countries { get; set; }
    }
}
