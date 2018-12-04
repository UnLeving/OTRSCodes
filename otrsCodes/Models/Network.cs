namespace otrsCodes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Network")]
    public partial class Network
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Network()
        {
            Codes = new HashSet<Code>();
        }

        public int Id { get; set; }

        public int CountryId { get; set; }

        public int ColorId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Code> Codes { get; set; }

        public virtual Color Color { get; set; }

        public virtual Country Country { get; set; }
    }
}
