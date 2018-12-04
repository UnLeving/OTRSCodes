namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Color
    {
        public Color()
        {
            Networks = new HashSet<Network>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(7)]
        public string Hex { get; set; }
        
        public ICollection<Network> Networks { get; set; }
    }
}
