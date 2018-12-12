namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Codes = new HashSet<Code>();
            Networks = new HashSet<Network>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public int Code { get; set; }

        public virtual ICollection<Code> Codes { get; set; }
        public virtual ICollection<Network> Networks { get; set; }
    }
}