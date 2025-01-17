using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class NewDbContext : ApplicationDbContext
    {
        public NewDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
            // DbSets for each model
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CompanyIndustry> CompanyIndustries { get; set; }
        public DbSet<CompanyMembership> CompanyMemberships { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCompany> ContactCompanies { get; set; }
        public DbSet<MembershipCancellation> MembershipCancellations { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
    }
    }

