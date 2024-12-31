namespace Recruitment_Platform.Models.HelperModels
{
    public class DashboardViewModel
    {
        public UserProfileViewModel UserProfile { get; set; }
        public List<OpportunityViewModel> LatestOpportunities { get; set; }
        public List<NotificationModel> Notifications { get; set; }
    }
}
