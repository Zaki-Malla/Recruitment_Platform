namespace Recruitment_Platform.Models.HelperModels
{
    public class PostJobViewModel
    {
        public string CompanyName { get; set; }
        public string WorkType { get; set; }
        public string CompanyAddress { get; set; }
        public string JobVacancies { get; set; }
        public string Description { get; set; }
        public string Skills { get; set; }
        public IFormFile JobImage { get; set; }
    }
}
