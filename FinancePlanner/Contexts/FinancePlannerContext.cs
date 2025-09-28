using FinancePlanner.Models.Expense;
using FinancePlanner.Models.Income;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Contexts
{
    public class FinancePlannerContext: DbContext
    {
        public FinancePlannerContext(DbContextOptions<FinancePlannerContext> options) : base(options) { 
        
        }
        public DbSet<Income> Income { get; set; } = default!;
        public DbSet<Expense> Expense { get; set; } = default!;
        public DbSet<FinancePlanner.Models.Investment> Investment { get; set; } = default!;

    }
}
