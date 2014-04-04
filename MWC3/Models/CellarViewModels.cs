namespace MWC3.Models
{
    using System;

    public class CellarViewModels
    {
    }

    public class CellarWineViewModel
    {
        public int WineId { get; set; }
        public string WineName { get; set; }
        public int? RegionId { get; set; }
        public string Region { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int? QualificationId { get; set; }
        public string Qualification { get; set; }
        public int Year { get; set; }
        public int? Stock { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public string ProducerCity { get; set; }
        public int ProducerCountryId { get; set; }
        public string ProducerCountry { get; set; }
        public decimal Alcohol { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CellarTransactionViewModel
    {
        public int TransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public int? DistributorId { get; set; }
        public string DistributorName { get; set; }
        public string DistributorCity { get; set; }
        public int DistributorCountryId { get; set; }
        public string DistributorCountry { get; set; }
        public int BottleTypeId { get; set; }
        public string BottleTypeName { get; set; }
        public int WineId { get; set; }
        public string WineName { get; set; }
        public int? RegionId { get; set; }
        public string Region { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int? QualificationId { get; set; }
        public string Qualification { get; set; }
        public int Year { get; set; }
        public decimal Alcohol { get; set; }
        public string Comment { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public string ProducerCity { get; set; }
        public int ProducerCountryId { get; set; }
        public string ProducerCountry { get; set; }
        public DateTime Date { get; set; }
        public int Multiplier { get; set; }

    }
}