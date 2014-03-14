namespace MWC3.Models
{
    using System.ComponentModel;
    using System;
    using System.Collections.Generic;
    public class BottleType
    {
        public int Id { get; set; }
        [DisplayName("Naam")]
        public string Name { get; set; }
        [DisplayName("Inhoud")]
        public int Capacity { get; set; }

        public string AddedBy { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } 
    }
}