
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;
using Recruitment_Platform.Repository;

namespace Recruitment_Platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<RoleModel> _roleManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IInformationRepository _informationRepository;

        public AccountController(SignInManager<UserModel> signInManager,
                                 UserManager<UserModel> userManager,
                                 RoleManager<RoleModel> roleManager,
                                 IInformationRepository informationRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _informationRepository = informationRepository;
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRole = roles.FirstOrDefault();

                    if (userRole == "Employer")
                    {
                        return RedirectToAction("EmployerDashboard", "Home"); 
                    }
                    else if (userRole == "User")
                    {
                        return RedirectToAction("Dashboard", "Home");
                    }
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRole = roles.FirstOrDefault();

                    HttpContext.Session.SetInt32("UserId", user.Id); 
                    HttpContext.Session.SetString("UserRole", userRole); 

                    if (userRole == "Employer")
                    {
                        return RedirectToAction("EmployerDashboard", "Home"); 
                    }
                    else if (userRole == "User")
                    {
                        return RedirectToAction("Dashboard", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "There is a problem with your ACCOUNT TYPE, contact support");
                return View("Login", model);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Login", model);
        }


        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRole = roles.FirstOrDefault();

                    if (userRole == "Employer")
                    {
                        return RedirectToAction("EmployerDashboard", "Home");
                    }
                    else if (userRole == "User")
                    {
                        return RedirectToAction("Dashboard", "Home");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel { UserName = model.Email, Email = model.Email, FullName = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var role = model.AccountType == "Employer" ? "Employer" : "User";
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new RoleModel { Name = role });
                    }
                    UserInformationModel newInfo = new UserInformationModel
                    {
                        UserId = user.Id,
                        FullName = user.FullName
                    };
                    _informationRepository.NewRow(newInfo);
                    await _userManager.AddToRoleAsync(user, role);

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Register", model);
        }

        public IActionResult CompleteProfile()
        {
            CompleteProfileViewModel model = new CompleteProfileViewModel();
            model.Id = HttpContext.Session.GetInt32("UserId").Value;
            model.FullName = _informationRepository.GetName(model.Id);
            model.Email = _informationRepository.GetEmail(model.Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfile(CompleteProfileViewModel model)
        {
            if (model.Image != null && model.Image.Length > 0 && model.Cv != null && model.Cv.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Users");
                Directory.CreateDirectory(uploadsFolder); 

                string uniqueFileNameIm = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Image.FileName);
                string uniqueFileNameCv = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Cv.FileName);
                string filePathIm = Path.Combine(uploadsFolder, uniqueFileNameIm);
                string filePathCv = Path.Combine(uploadsFolder, uniqueFileNameCv);

                if (model.Cv.ContentType != "application/pdf")
                {
                    ModelState.AddModelError("", "The uploaded file is not a valid PDF document.");
                    return View(model);
                }

                using (var fileStream = new FileStream(filePathIm, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                using (var fileStream = new FileStream(filePathCv, FileMode.Create))
                {
                    await model.Cv.CopyToAsync(fileStream);
                }

                UserInformationModel NewInfo = new UserInformationModel
                {
                    UserId = model.Id,
                    FullName = model.FullName,
                    Address = model.Country + ", " + model.City,
                    Skills = model.Skills?.Split(',').Select(v => v.Trim()).ToList(),
                    ImagePath = Path.Combine("Users", uniqueFileNameIm),
                    CvPath = Path.Combine("Users", uniqueFileNameCv),
                    Age = model.Age
                };

                _informationRepository.UpdateInformation(NewInfo);

                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Please upload both a valid image and a PDF CV.");
                return View(model);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }


    }
}
