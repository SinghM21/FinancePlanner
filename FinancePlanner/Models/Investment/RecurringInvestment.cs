namespace FinancePlanner.Models.Investment
{
    public class RecurringInvestment : Investment
    {
        public FrequencyType Frequency { get; set; }

        public int? FrequencyInDays { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime? EndDate { get; set; }
    }
}
