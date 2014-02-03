using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Qualification
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Qualification")]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int CountryId { get; set; }
        public int? RegionId { get; set; }
        public string AddedBy { get; set; }
        public DateTime Timestamp { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }
}