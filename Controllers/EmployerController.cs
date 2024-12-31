using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;
using Recruitment_Platform.Repository;

namespace Recruitment_Platform.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerController : Controller
    {
        private readonly IOpportunitiesRepository _opportunitiesRepository;
        private readonly IOffersRepository _offersRepository;
        public EmployerController(IOpportunitiesRepository opportunitiesRepository, IOffersRepository offersRepository)
        {
            _opportunitiesRepository = opportunitiesRepository;
            _offersRepository = offersRepository;
        }

        public IActionResult MyJobPosts()
        {
            List<MyJobPostsViewModel> model = _opportunitiesRepository
                .GetAll(HttpContext.Session.GetInt32("UserId").Value)
                .Select(o => new MyJobPostsViewModel
                {
                    Id = o.Id,
                    CompanyName = o.CompanyName,
                    WorkType = o.WorkType,
                    PublishDate = o.PublishDate,
                    ApplicationsReceived = _opportunitiesRepository.OffersNumber(o.Id),
                    Status = o.Status
                })
                .ToList();
            return View(model);
        }
        public IActionResult PostNewJob()
        {
            return View(new PostJobViewModel());
        }
        [HttpPost]
        public IActionResult JobDetails(int JobId)
        {
            return View(_opportunitiesRepository.GetOne(JobId));
        }

        [HttpPost]
        public async Task<IActionResult> PostJob(PostJobViewModel NewJob)
        {
            string imagePath = null;

            if (NewJob.JobImage != null && NewJob.JobImage.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); // إنشاء المجلد إذا لم يكن موجودًا
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(NewJob.JobImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await NewJob.JobImage.CopyToAsync(fileStream);
                }

                imagePath = Path.Combine("images", uniqueFileName);
            }

            var jobVacanciesList = NewJob.JobVacancies?.Split(',').Select(v => v.Trim()).ToList();
            var skillsList = NewJob.Skills?.Split(',').Select(s => s.Trim()).ToList();

            OpportunitiesOfferedModel model = new OpportunitiesOfferedModel
            {
                PublisherId = HttpContext.Session.GetInt32("UserId").Value,
                CompanyName = NewJob.CompanyName,
                WorkType = NewJob.WorkType,
                CompanyAddress = NewJob.CompanyAddress,
                JobVacancies = jobVacanciesList,
                Description = NewJob.Description,
                Skills = skillsList,
                ImagePath = imagePath,
                PublishDate = DateTime.Now,
                Status = "Active"
            };
            _opportunitiesRepository.AddNewOpportunities(model);

            return RedirectToAction("EmployerDashboard", "Home");
        }
        [HttpPost]
        public IActionResult ViewOffer(int OfferId)
        {
            OfferViewModel offer = _offersRepository.GetOfferById(OfferId);
            return View(offer);
        }

        [HttpPost]
        public IActionResult AcceptOffer(int OfferId)
        {
            _offersRepository.AcceptOffer(OfferId);
            return RedirectToAction("EmployerDashboard", "Home");
        }

        [HttpPost]
        public IActionResult RejectOffer(int OfferId)
        {
            _offersRepository.RejectOffer(OfferId);
            return RedirectToAction("EmployerDashboard", "Home");
        }
    }
}
