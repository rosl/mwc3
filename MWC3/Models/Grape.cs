namespace MWC3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Grape
    {
        public int Id { get; set; }
        [DisplayName("Druif")]
        public string Name { get; set; }
        [DisplayName("Parent druif")]
        public int ParentId { get; set; }
        [DisplayName("Kleur")]
        public int ColorId { get; set; }
        public string AddedBy { get; set; }
        public DateTime TimeStamp { get; set; }

        [NotMapped]
        public virtual Grape ParentGrape { get; set; }
        [NotMapped]
        public virtual List<Grape> ChildGrapes { get; set; }
        [NotMapped]
        public virtual GrapeColor GrapeColor { get; set; }

        public virtual ICollection<Wine> Wines { get; set; }
    }
}