namespace FinancePlanner.Models
{
    public class Outcome
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public int? Quantity { get; set; }

        public int Cost { get; set; }
    }
}
