namespace Recruitment_Platform.Models.HelperModels
{
    public class OpportunitiesOfferedViewModel
    {
        public int PublisherId { get; set; }
        public int UserId { get; set; }
        public int OpportunityId { get; set; }
        public OpportunitiesOfferedModel Opportunity { get; set; }
        public UserInformationModel UserInformation { get; set; }
        public string WhyYou { get; set; }


    }
}
