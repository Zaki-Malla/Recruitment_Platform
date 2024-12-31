using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;
using Microsoft.EntityFrameworkCore;

namespace Recruitment_Platform.Repository
{
    public class OffersRepository : IOffersRepository
    {
        private readonly rpContext _context;
        private readonly IOpportunitiesRepository _opportunitiesRepository;
        private readonly INotificationRepository _notificationRepository;

        public OffersRepository(rpContext context, IOpportunitiesRepository opportunitiesRepository , INotificationRepository notificationRepository)
        {
            _context = context;
            _opportunitiesRepository = opportunitiesRepository;
            _notificationRepository = notificationRepository;
        }
        public void AddNewOffer(OffersForOpportunitiesModel model)
        {
            _context.TbOffersForOpportunities.Add(model);
            _context.SaveChanges();
        }
        public List<OffersForOpportunitiesModel> GetLastFourOffers(int EmployerId)
        {
            return _context.TbOffersForOpportunities
                           .Where(o => o.PublisherId == EmployerId) 
                           .OrderByDescending(o => o.OfferDate)    
                           .Take(4)                               
                           .ToList();                            
        }
        public List<OffersForOpportunitiesModel> GetAllOffers(int EmployerId)
        {
            return _context.TbOffersForOpportunities
                           .Where(o => o.PublisherId == EmployerId)
                           .OrderByDescending(o => o.OfferDate)
                           .ToList();
        }
        public int GetTotalApplicationsForUserOpportunities(int userId)
        {
            return _context.TbOffersForOpportunities
                .Count(app => _context.TbOpportunitiesOffered
                    .Any(op => op.Id == app.OpportunityId && op.PublisherId == userId));
        }
        public int GetTotalAcceptedApplicationsForUserOpportunities(int userId)
        {
            return _context.TbOffersForOpportunities
                .Count(app => _context.TbOpportunitiesOffered
                    .Any(op => op.Id == app.OpportunityId && op.PublisherId == userId && app.Status == "Accept"));
        }
        public OfferViewModel GetOfferById(int OfferId)
        {
            OffersForOpportunitiesModel OfferModel = _context.TbOffersForOpportunities.Find(OfferId);
            OpportunitiesOfferedModel opportunity = _context.TbOpportunitiesOffered.Find(OfferModel.OpportunityId);
            OfferViewModel model = new OfferViewModel
            { OfferId = OfferId,
            ApplicantName = OfferModel.FullName,
                ApplicantAge = OfferModel.Age,
                ApplicantSkills = OfferModel.UserSkills,
                ApplicantCvPath = OfferModel.UserCv,
                ApplicantImage = OfferModel.ImagePath,
                WhyYou = OfferModel.WhyYou,
                OfferDate = OfferModel.OfferDate,
                Status = OfferModel.Status,
                CompanyName = opportunity.CompanyName,
                WorkType = opportunity.WorkType,
                CompanyAddress = opportunity.CompanyAddress,
                JobVacancies = opportunity.JobVacancies,
                JobDescription = opportunity.Description,
                JobSkills = opportunity.Skills,
                JobImagePath = opportunity.ImagePath
            };

            return model;
        }
        public OffersForOpportunitiesModel GetOfferByUserAndOpportunity(int userId, int opportunityId)
        {
            return _context.TbOffersForOpportunities
                           .FirstOrDefault(o => o.UserId == userId && o.OpportunityId == opportunityId);
        }
        public void AcceptOffer(int OfferId)
        {
            OffersForOpportunitiesModel offer = _context.TbOffersForOpportunities.Find(OfferId);
            offer.Status = "Accept";
            NotificationModel Notification = new NotificationModel
            {
                UserId = offer.UserId,
            Title = "Your job application has been accepted.",
            Description = "Congratulations, your job application to " + _opportunitiesRepository.GetOne(offer.OpportunityId).CompanyName + " has been accepted, wait for the company to contact you.",
            Date = DateTime.Now
            };
            _notificationRepository.AddNotification(Notification);
            _context.SaveChanges();
        }
        public void RejectOffer(int OfferId)
        {
            OffersForOpportunitiesModel offer = _context.TbOffersForOpportunities.Find(OfferId);
            offer.Status = "Reject";
            NotificationModel Notification = new NotificationModel
            {
                UserId = offer.UserId,
                Title = "Your job application has been rejected.",
                Description = "Unfortunately, your job application to " + _opportunitiesRepository.GetOne(offer.OpportunityId).CompanyName + " has been rejected, try again later.",
                Date = DateTime.Now
            };
            _notificationRepository.AddNotification(Notification);
            _context.SaveChanges();
        }

    }
}
