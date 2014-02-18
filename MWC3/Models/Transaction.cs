using System;

namespace MWC3.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Transaction
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int TransactionTypeId { get; set; }
        public string UserId { get; set; }
        public int? BusinessId { get; set; }
        public int BottleTypeId { get; set; }
        public int WineId { get; set; }
        public int Year { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string AddedBy { get; set; }
        public DateTime TimeStamp { get; set; }
        [NotMapped]
        public decimal TotalPrice { get; set; }

        [ForeignKey("TransactionTypeId")]
        public virtual TransactionType TransactionType { get; set; }
        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }
        [ForeignKey("BottleTypeId")]
        public virtual BottleType BottleType { get; set; }
        [ForeignKey("WineId")]
        public virtual Wine Wine { get; set; }
    }
}