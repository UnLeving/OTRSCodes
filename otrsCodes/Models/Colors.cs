namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public partial class Colors
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Colors()
        {
            Networks = new HashSet<Networks>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(7)]
        public string Hex { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Networks> Networks { get; set; }
    }
}
