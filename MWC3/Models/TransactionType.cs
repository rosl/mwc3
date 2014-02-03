using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Multiplier { get; set; }
        public string LanguageCode { get; set; }
        public int ParentId { get; set; }
        public string AddedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } 
    }
}