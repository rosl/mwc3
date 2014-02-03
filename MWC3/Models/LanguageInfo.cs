using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Models
{
    public class LanguageInfo
    {
        public int Id { get; set; }
        public string LanguageName { get; set; }

        public string LanguageCode { get; set; }

        public string CultureCode { get; set; }

        public bool Enabled { get; set; }
    }
}