namespace otrsCode_netCore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Color")]
    public class Color
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^#[a-z0-9]{1,7}$", ErrorMessage = "Bad format. Ex: #123456")]
        public string Hex { get; set; }

        public virtual ICollection<Network> Networks { get; set; }
    }
}