namespace Recruitment_Platform.Models.HelperModels
{
    public class OfferViewModel
    {
        public int OfferId { get; set; }
        public string ApplicantName { get; set; }
        public int ApplicantAge { get; set; }
        public List<string> ApplicantSkills { get; set; }
        public string ApplicantCvPath { get; set; }
        public string ApplicantImage { get; set; }
        public string WhyYou {  get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }
        public string WorkType { get; set; }
        public string CompanyAddress { get; set; }
        public List<string> JobVacancies { get; set; }
        public string JobDescription { get; set; }
        public List<string> JobSkills { get; set; }
        public string JobImagePath { get; set; }
        public DateTime OfferDate { get; set; }
    }
}
