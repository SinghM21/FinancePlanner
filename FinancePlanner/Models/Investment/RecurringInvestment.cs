namespace FinancePlanner.Models.Investment
{
    public class RecurringInvestment : Investment
    {
        public FrequencyType Frequency { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
