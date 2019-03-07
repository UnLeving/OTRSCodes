namespace otrsCode_netCore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Code")]
    public class Code
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
        [Required]
        public string R { get; set; }

        public int CountryId { get; set; }
        public int NetworkId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Network Network { get; set; }
    }
}