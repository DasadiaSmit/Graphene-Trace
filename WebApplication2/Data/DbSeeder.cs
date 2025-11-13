using GrapheneTrace.Models;

namespace GrapheneTrace.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Name = "Test User", Email = "test@demo.com", Password = "1234", Role = "Patient" },
                    new User { Name = "Clinician One", Email = "clinician@demo.com", Password = "1234", Role = "Clinician" }
                );
                context.SaveChanges();
            }
        }
    }
}
