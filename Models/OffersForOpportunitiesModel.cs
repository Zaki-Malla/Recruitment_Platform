using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Recruitment_Platform.Models
{
    public class OffersForOpportunitiesModel
    {
        [Key]
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
        public int OpportunityId { get; set; }
        [ForeignKey("OpportunityId")]
        public virtual OpportunitiesOfferedModel Opportunity { get; set; }
        public List<string> UserSkills { get; set; }
        public string Status { get; set; } = "Pending";
        public string WhyYou { get; set; }
        public DateTime OfferDate { get; set; } = DateTime.Now;
        public string UserCv {  get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string FullName { get; set; }
    }
}
