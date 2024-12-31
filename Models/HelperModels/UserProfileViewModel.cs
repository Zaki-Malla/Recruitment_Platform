namespace Recruitment_Platform.Models.HelperModels
{
    public class UserProfileViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? ImagePath { get; set; }
        public string? CvPath { get; set; }
        public string? Address { get; set; }

        public List<string> Skills { get; set; } = new List<string>();

        public int AppliedOpportunitiesCount { get; set; }
        public int AcceptedOpportunitiesCount { get; set; }

    }
}
