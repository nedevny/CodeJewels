using CodeJewelModels;
using System.Data.Entity;

namespace CodeJewelData
{
    public class CodeContext : DbContext
    {
        public CodeContext()
            : base("CodeDb")
        {
        }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CodeJewel> CodeJewels { get; set; }
    }
}