namespace MWC3.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Business
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Business")]
        public string Name { get; set; }
        [DisplayName("Owned by")]
        public string Owner{ get; set; }
        [EmailAddressAttribute]
        public string Email { get; set; }
        public string Url { get; set; }
        public string Phone { get; set; }
        public bool IsDistributor { get; set; }
        public bool IsProducer { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public string AddedBy { get; set; }
        public DateTime Timestamp { get; set; }
    }
}