using FinancePlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Contexts
{
    public class FinancePlannerContext: DbContext
    {
        public FinancePlannerContext(DbContextOptions<FinancePlannerContext> options) : base(options) { 
        
        }

    }
}
