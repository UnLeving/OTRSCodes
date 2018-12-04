namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Zones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zones()
        {
            Codes = new HashSet<Codes>();
        }
        [Key]
        public int Id { get; set; }
        
        public int CountryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Codes> Codes { get; set; }
        [ForeignKey("CountryId")]
        public virtual Countries Countries { get; set; }
    }
}
