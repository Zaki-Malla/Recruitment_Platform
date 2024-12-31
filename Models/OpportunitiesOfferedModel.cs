using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Recruitment_Platform.Models
{
    public class OpportunitiesOfferedModel
    {
        [Key]
        public int Id { get; set; }
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public virtual UserModel User { get; set; }
        public string CompanyName { get; set; }
        public string WorkType { get; set; }
        public string CompanyAddress { get; set; }
        public List<string> JobVacancies { get; set; }
        public string Description { get; set; }
        public List<string> Skills { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; } = "Active";

        public DateTime PublishDate { get; set; } = DateTime.Now;

        public virtual List<OffersForOpportunitiesModel> Offers { get; set; }


    }
}
