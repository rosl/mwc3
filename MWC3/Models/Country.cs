namespace MWC3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        public int Id { get; set; }
        [DisplayName("Country")]
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        [DisplayName("Parent Country")]
        public int ParentId { get; set; }
        public string AddedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        [NotMapped]
        public virtual Country ParentCountry { get; set; }
        public virtual ICollection<Qualification> Qualifications { get; set; }
        public virtual ICollection<Wine> Wines { get; set; }
    }
}