namespace otrsCodes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Zone
    {
        public Zone()
        {
            Codes = new HashSet<Code>();
        }
        [Key]
        public int Id { get; set; }
        
        public int CountryId { get; set; }

        public ICollection<Code> Codes { get; set; }
        [ForeignKey("CountryId")]
        public Country Countries { get; set; }
    }
}
