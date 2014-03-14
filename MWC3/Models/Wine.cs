namespace MWC3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Wine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RegionId { get; set; }
        public int WineColorId { get; set; }
        public int CountryId { get; set; }
        public int? QualificationId { get; set; }
        public int BusinessId { get; set; }
        public bool IsSparkling { get; set; }
        public bool IsFortified { get; set; }
        public bool IsSweet { get; set; }
        public string AddedBy { get; set; }
        public DateTime TimeStamp { get; set; }
        [NotMapped]
        [DisplayName("MyGrapes")]
        public int[] GrapeIds { get; set; }
        public virtual ICollection<Grape> Grapes { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
        [ForeignKey("WineColorId")]
        public virtual WineColor WineColor { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        [ForeignKey("QualificationId")]
        public virtual Qualification Qualification { get; set; }
        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } 
    }
}