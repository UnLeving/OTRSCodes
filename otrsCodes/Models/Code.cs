namespace otrsCodes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Code")]
    public partial class Code
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Value { get; set; }

        public int CountryId { get; set; }

        public int NetworkId { get; set; }

        public int ZoneId { get; set; }

        public virtual Country Country { get; set; }

        public virtual Network Network { get; set; }

        public virtual Zone Zone { get; set; }
    }
}
