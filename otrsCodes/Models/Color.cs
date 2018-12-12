namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Color")]
    public partial class Color
    {
        public Color()
        {
            Networks = new HashSet<Network>();
        }

        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^#[a-z0-9]{1,7}$", ErrorMessage = "Bad format. Ex: #123456")]
        public string Hex { get; set; }

        public virtual ICollection<Network> Networks { get; set; }
    }
}