using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Platform.Models
{
    public class UserModel : IdentityUser<int>
    {
        [Required]
        public string FullName { get; set; }
        public virtual UserInformationModel Information { get; set; }
        public virtual List<NotificationModel> Notifications { get; set; }
        public virtual List<OffersForOpportunitiesModel> Offers { get; set; }
        public virtual List<OpportunitiesOfferedModel> Opportunities { get; set; }

    }


}
