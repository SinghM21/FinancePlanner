using FinancePlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancePlanner.Contexts
{
    public class FinancePlannerContext: DbContext
    {
        public FinancePlannerContext(DbContextOptions<FinancePlannerContext> options) : base(options) { 
        
        }
        public DbSet<FinancePlanner.Models.Income> Income { get; set; } = default!;
        public DbSet<FinancePlanner.Models.Outcome> Outcome { get; set; } = default!;
        public DbSet<FinancePlanner.Models.Investment> Investment { get; set; } = default!;

    }
}
