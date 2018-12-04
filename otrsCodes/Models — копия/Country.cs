namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Country
    {
        public Country()
        {
            Codes = new HashSet<Code>();
            Networks = new HashSet<Network>();
            Zones = new HashSet<Zone>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public ICollection<Code> Codes { get; set; }
        public ICollection<Network> Networks { get; set; }
        public ICollection<Zone> Zones { get; set; }
    }
}
