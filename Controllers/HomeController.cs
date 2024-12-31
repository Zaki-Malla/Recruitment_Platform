using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Platform.Models.HelperModels;
using Recruitment_Platform.Repository;

namespace Recruitment_Platform.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOpportunitiesRepository _opportunitiesRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IOffersRepository _offersRepository;

        public HomeController (IAccountRepository accountRepository, IOpportunitiesRepository opportunitiesRepository, INotificationRepository notificationRepository, IOffersRepository offersRepository)
        {
            _accountRepository = accountRepository; _opportunitiesRepository = opportunitiesRepository; _notificationRepository = notificationRepository;
            _offersRepository = offersRepository;
        }
        [Authorize(Roles = "Employer")]
        public IActionResult EmployerDashboard()
        {
            int employerId = HttpContext.Session.GetInt32("UserId").Value;
            ViewBag.LastFourApplications = _offersRepository.GetLastFourOffers(employerId);
            ViewBag.LastThreeOppo = _opportunitiesRepository.GetLastThree(employerId);
            ViewBag.UserFullName = _accountRepository.GetNameById(employerId);
            ViewBag.ActiveJobs = _opportunitiesRepository.GetActiveOpportunitiesCountByUser(employerId);
            ViewBag.ApplicationReceived = _offersRepository.GetTotalApplicationsForUserOpportunities(employerId);
            ViewBag.AcceptedApplication = _offersRepository.GetTotalAcceptedApplicationsForUserOpportunities(employerId);
            return View();
        }
        [Authorize(Roles = "User")]

        public IActionResult Dashboard()
        {
            if(_accountRepository.GetUserInfo(HttpContext.Session.GetInt32("UserId").Value).CvPath == null)
            {
                return RedirectToAction("CompleteProfile","Account");
            }

            else { 
            var viewModel = new DashboardViewModel
            {
                UserProfile = _accountRepository.GetUserInfo(HttpContext.Session.GetInt32("UserId").Value)
                ,
                LatestOpportunities = _opportunitiesRepository.GetLastThreeActive()
                    .Select(o => new OpportunityViewModel
                    {
                        CompanyName = o.CompanyName,
                        WorkType = o.WorkType,
                        Description = o.Description,
                        PublishDate = o.PublishDate
                    }).ToList(),
                Notifications = _notificationRepository.GetAllForUser(HttpContext.Session.GetInt32("UserId").Value)
            };

            return View(viewModel);
            }
        }

    }
}
