using FinancePlanner.Models.Investment;
using System;

namespace FinancePlanner.Helpers
{
    public static class FrequencyTypeHelper
    {
        public static int GetAnnualMultiplier(FrequencyType frequency)
        {
            return frequency switch
            {
                FrequencyType.Daily => 365,
                FrequencyType.Weekly => 52,
                FrequencyType.BiWeekly => 26,
                FrequencyType.Monthly => 12,
                FrequencyType.Quarterly => 4,
                FrequencyType.SemiAnnually => 2,
                FrequencyType.Annually => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(FrequencyType))
            };
        }
    }
}