using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;
using Recruitment_Platform.Repository;
using System.Net;

namespace Recruitment_Platform.Controllers
{
    public class OpportunityController : Controller
    {
        private readonly IOpportunitiesRepository _opportunitiesRepository;
        private readonly IInformationRepository _informationRepository;
        private readonly IOffersRepository _offersRepository;
        private readonly INotificationRepository _notificationRepository;

        public OpportunityController(IOpportunitiesRepository opportunitiesRepository, IInformationRepository informationRepository, IOffersRepository offersRepository, INotificationRepository notificationRepository)
        {
            _opportunitiesRepository = opportunitiesRepository;
            _informationRepository = informationRepository;
            _offersRepository = offersRepository;
            _notificationRepository = notificationRepository;
        }
        [Authorize(Roles = "User")]
        public IActionResult AllOpportunities()
        {
            return View(_opportunitiesRepository.GetAllActive());
        }
        [Authorize(Roles = "User")]
        public IActionResult LoadMore(int offset)
        {
            var opportunities = _opportunitiesRepository.LoadMore(offset);

            if (!opportunities.Any())
            {
                return Content(""); 
            }

            return PartialView("_OpportunityPartial", opportunities);
        }
        [Authorize(Roles = "User")]
        public ActionResult ViewOpportuny(int id)
        {
            OpportunitiesOfferedViewModel Offer = new OpportunitiesOfferedViewModel
            {
                UserId = HttpContext.Session.GetInt32("UserId").Value,
                OpportunityId = id,
                Opportunity = _opportunitiesRepository.GetOne(id),
                PublisherId = _opportunitiesRepository.GetOne(id).PublisherId,
                WhyYou = string.Empty,
                UserInformation = _informationRepository.GetUserInformation(HttpContext.Session.GetInt32("UserId").Value)
            };
            return View(Offer);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult SubmitApplication(ApplicationModel model)
        {
            var existingOffer = _offersRepository.GetOfferByUserAndOpportunity(model.ApplicantId, model.OpportunityId);

            if (existingOffer != null)
            {
                TempData["ErrorMessage"] = "You have already applied for this opportunity.";
                return RedirectToAction("ViewOpportuny", "Opportunity", new { id = model.OpportunityId });
            }

            OffersForOpportunitiesModel newOffer = new OffersForOpportunitiesModel
            {
                PublisherId = model.PublisherId,
                UserId = model.ApplicantId,
                OpportunityId = model.OpportunityId,
                UserSkills = model.Skills,
                Status = "Pending",
                WhyYou = model.Reason,
                OfferDate = DateTime.Now,
                UserCv = model.CvPath,
                ImagePath = model.ImagePath,
                Address = model.Address,
                Age = model.Age,
                FullName = model.FullName
            };
            NotificationModel Notification = new NotificationModel
            {
                UserId = model.ApplicantId,
                Title = "Submit job application successfully.",
                Description = "Your job application has been successfully sent to " + _opportunitiesRepository.GetOne(model.OpportunityId).CompanyName + ".",
                Date = DateTime.Now
            };
            _offersRepository.AddNewOffer(newOffer);
            _notificationRepository.AddNotification(Notification);

            return RedirectToAction("Dashboard", "Home");
        }


        [HttpPost]
        public IActionResult CloseOpportunity(int OpportunitiesId)
        {
            _opportunitiesRepository.CloseOpportunity(OpportunitiesId);
            return RedirectToAction("MyJobPosts", "Employer");
        }

        [HttpPost]
        public IActionResult ActiveOpportunity(int OpportunitiesId)
        {
            _opportunitiesRepository.ActiveOpportunity(OpportunitiesId);
            return RedirectToAction("MyJobPosts", "Employer");
        }
        [Authorize(Roles = "Employer")]
        public IActionResult AllApplications()
        {
            int EmployerId = HttpContext.Session.GetInt32("UserId").Value;
            ViewBag.Offers = _offersRepository.GetAllOffers(EmployerId);
            return View();
        }


    }
}
