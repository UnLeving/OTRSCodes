namespace otrsCodes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Color")]
    public partial class Color
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Color()
        {
            Networks = new HashSet<Network>();
        }

        public int Id { get; set; }

        [Required]
        //[StringLength(7, MinimumLength =7)]
        [RegularExpression(@"^#[a-z0-9]{1,7}$", ErrorMessage = "Bad format. Ex: #123456")]
        public string Hex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Network> Networks { get; set; }
    }
}
