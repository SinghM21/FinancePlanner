namespace FinancePlanner.Models.Income
{
    public class Income
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public IncomeType Type { get; set; }

        public int Amount { get; set; }
    }
}
