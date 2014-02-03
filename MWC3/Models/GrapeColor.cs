using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GrapeColor
    {
        [Key]
        public int Id { get; set; }

        public int ColorId { get; set; }
        public string Name { get; set; }

       [StringLength(2, ErrorMessage = "Description Max Length is 2")]
        public string LanguageCode { get; set; }
    }
}