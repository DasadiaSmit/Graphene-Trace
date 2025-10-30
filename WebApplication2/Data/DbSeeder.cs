using System;
using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        FullName = "Admin User",
                        Email = "admin@graphenetrace.com",
                        PasswordHash = "admin123",
                        Role = "Admin",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    },
                    new User
                    {
                        FullName = "Clinician User",
                        Email = "clinician@graphenetrace.com",
                        PasswordHash = "clinician123",
                        Role = "Clinician",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    },
                    new User
                    {
                        FullName = "Patient User",
                        Email = "patient@graphenetrace.com",
                        PasswordHash = "patient123",
                        Role = "Patient",
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    }
                );

                context.SaveChanges();
                Console.WriteLine("[SEED] Inserted 3 users.");
            }
            else
            {
                Console.WriteLine("[SEED] Skipped (users exist).");
            }
            Console.WriteLine($"[SEED] Users count after: {context.Users.Count()}");
        }
    }
    }
    

