using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class Initializer
    {
        public static void Initialize(IServiceProvider serviceProvider, bool deleteDatabase, bool useMigrations, bool seedSampleData)
        {
            // Get the NewDbContext from the service provider
            using var context = serviceProvider.GetRequiredService<NewDbContext>();

            if (deleteDatabase)
            {
                // Delete the database if requested
                Console.WriteLine("Deleting the database...");
                context.Database.EnsureDeleted();
                Console.WriteLine("Database deleted.");
            }

            if (useMigrations)
            {
                // Apply any pending migrations
                Console.WriteLine("Applying database migrations...");
                context.Database.Migrate();
                Console.WriteLine("Database migrations applied.");
            }
            else
            {
                // Ensure the database is created without migrations
                Console.WriteLine("Ensuring database is created...");
                context.Database.EnsureCreated();
                Console.WriteLine("Database created (without migrations).");
            }

            if (seedSampleData)
            {
                // Seed the database with sample data
                Console.WriteLine("Seeding database...");
                SeedData(context);
                Console.WriteLine("Sample data seeded.");
            }
        }

        private static void SeedData(NewDbContext context)
        {
            // Check if the database already contains data
            if (context.Companies.Any())
            {
                Console.WriteLine("Database already contains data. Skipping seeding.");
                return;
            }

            Console.WriteLine("Seeding sample data...");

            // Seed Companies
            var companies = new[]
            {
        new Company { Name = "ABC Company 1", MembershipType = "Associate" },
        new Company { Name = "ABC Company 2", MembershipType = "Local Industrial" },
        new Company { Name = "ABC Company 3", MembershipType = "Government & Education" },
        new Company { Name = "Tech Solutions Inc.", MembershipType = "Technology" },
        new Company { Name = "Green Energy Corp.", MembershipType = "Energy" },
        new Company { Name = "BuildRight Constructions", MembershipType = "Construction" },
        new Company { Name = "HealthCare Plus", MembershipType = "Healthcare" },
        new Company { Name = "AutoFix Mechanics", MembershipType = "Automotive" },
        new Company { Name = "NextGen Software", MembershipType = "Technology" },
        new Company { Name = "Visionary Designs", MembershipType = "Design" },
        new Company { Name = "EcoFriendly Supplies", MembershipType = "Manufacturing" },
        new Company { Name = "SafeNet Security", MembershipType = "Security" },
        new Company { Name = "Bright Futures Education", MembershipType = "Education" },
        new Company { Name = "Niagara IT Solutions", MembershipType = "Technology" },
        new Company { Name = "Streamline Logistics", MembershipType = "Logistics" },
        new Company { Name = "Creative Minds Studio", MembershipType = "Design" },
        new Company { Name = "Global Trade Ltd.", MembershipType = "International Trade" },
        new Company { Name = "Pure Aqua Bottling", MembershipType = "Beverages" },
        new Company { Name = "Niagara Industrial Co.", MembershipType = "Manufacturing" },
        new Company { Name = "Green Earth Landscaping", MembershipType = "Landscaping" },
        new Company { Name = "Precision Engineering", MembershipType = "Engineering" },
        new Company { Name = "Innovative Robotics", MembershipType = "Technology" },
        new Company { Name = "Harmony Wellness Center", MembershipType = "Healthcare" },
        new Company { Name = "Elite Travel Agency", MembershipType = "Travel" },
        new Company { Name = "FreshFarm Organics", MembershipType = "Agriculture" },
        new Company { Name = "Skyline Architecture", MembershipType = "Architecture" },
        new Company { Name = "SuperTech Gadgets", MembershipType = "Retail" },
        new Company { Name = "Apex Financial Services", MembershipType = "Finance" },
        new Company { Name = "BlueWater Marine", MembershipType = "Marine" },
        new Company { Name = "Redline Motors", MembershipType = "Automotive" },
        new Company { Name = "SmartHome Innovations", MembershipType = "Technology" },
        new Company { Name = "Evergreen Forestry", MembershipType = "Forestry" },
        new Company { Name = "Urban Eats Catering", MembershipType = "Hospitality" },
        new Company { Name = "Summit Outdoor Gear", MembershipType = "Retail" },
        new Company { Name = "Northern Lights Media", MembershipType = "Media" },
        new Company { Name = "Peak Performance Fitness", MembershipType = "Healthcare" },
        new Company { Name = "Unity Publishing", MembershipType = "Publishing" },
        new Company { Name = "Vista Electronics", MembershipType = "Technology" },
        new Company { Name = "Dynamic Advertising", MembershipType = "Marketing" },
        new Company { Name = "AquaMarine Adventures", MembershipType = "Tourism" },
        new Company { Name = "Alpha Research Labs", MembershipType = "Science" },
        new Company { Name = "Bright Horizons Daycare", MembershipType = "Education" },
        new Company { Name = "SecureLink Communications", MembershipType = "Telecommunications" },
        new Company { Name = "Infinity Clothing", MembershipType = "Retail" },
        new Company { Name = "Quantum Computing Co.", MembershipType = "Technology" }
    };
            context.Companies.AddRange(companies);
            context.SaveChanges();
            Console.WriteLine($"Added {companies.Length} companies.");

            // Seed Addresses
            var addresses = companies.Select((company, index) => new Address
            {
                CompanyId = company.Id,
                City = $"City{index + 1}",
                State = "ON",
                Country = "Canada"
            }).ToArray();
            context.Addresses.AddRange(addresses);
            context.SaveChanges();
            Console.WriteLine($"Added {addresses.Length} addresses.");

            Console.WriteLine("Seeding completed.");
        }
    }
}

