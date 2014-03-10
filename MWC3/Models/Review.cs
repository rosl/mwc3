namespace MWC3.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int WineId { get; set; }
        public int Year { get; set; }

        public string ReviewerId { get; set; }

        public double Score { get; set; }

        public int ReviewTypeId { get; set; }

        public virtual Wine Wine { get; set; }
        public virtual ReviewType ReviewType { get; set; }
    }
}