using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApplication2.Data   // ← keep this namespace same as your project
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GrapheneTraceDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
