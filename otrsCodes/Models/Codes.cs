namespace otrsCodes.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Codes
    {
        public int Id { get; set; }

        [Required]
        public string[] Value { get; set; }
        [Required]
        public string Zone { get; set; }

        public int CountryId { get; set; }
        public int NetworkId { get; set; }
    }
}