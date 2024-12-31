namespace Recruitment_Platform.Models.HelperModels
{
    public class ApplicationModel
    {
        public int ApplicantId { get; set; }
        public int PublisherId { get; set; }
        public string FullName { get; set; } 
        public string ImagePath { get; set; } 
        public int Age { get; set; } 
        public string Address { get; set; }
        public List<string> Skills { get; set; } 
        public string Reason { get; set; } 
        public string CvPath { get; set; } 
        public int OpportunityId { get; set; } 
    }

}
