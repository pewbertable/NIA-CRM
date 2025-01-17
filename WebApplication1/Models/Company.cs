using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string MembershipType { get; set; }

        // Navigation Properties
        public ICollection<Address> Addresses { get; set; }
        public ICollection<CompanyIndustry> Industries { get; set; }
        public ICollection<CompanyMembership> Memberships { get; set; }
        public ICollection<ContactCompany> ContactCompanies { get; set; }
        public ICollection<MembershipCancellation> MembershipCancellations { get; set; }
        public ICollection<Opportunity> Opportunities { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
    }
}

