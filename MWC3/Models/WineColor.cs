using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WineColor
    {
        public int Id { get; set; }
        [DisplayName("WineColor")]
        public string Name { get; set; }

        [DisplayName("Parent WineColor")]
        public int ParentId { get; set; }

        [StringLength(2, ErrorMessage = "Description Max Length is 2")]
        public string LanguageCode { get; set; }

        [NotMapped]
        public virtual WineColor ParentWineColor { get; set; }
    }
}