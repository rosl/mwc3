namespace MWC3.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Region
    {
        public int Id { get; set; }
        [DisplayName("Region")]
        public string Name { get; set; }
        public string AddedBy { get; set; }
        public DateTime Timestamp { get; set; }
        [DisplayName("Country")]
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}