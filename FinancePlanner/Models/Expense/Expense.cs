namespace FinancePlanner.Models.Expense
{
    public class Expense
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public ExpenseType Type { get; set; }

        public int? Quantity { get; set; }

        public int Cost { get; set; }
    }
}
