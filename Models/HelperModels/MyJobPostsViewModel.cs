namespace Recruitment_Platform.Models.HelperModels
{
    public class MyJobPostsViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string WorkType { get; set; }
        public DateTime PublishDate { get; set; }
        public int ApplicationsReceived { get; set; }
        public string Status { get; set; }
    }
}
