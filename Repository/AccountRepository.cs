using Microsoft.AspNetCore.Identity;
using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;

namespace Recruitment_Platform.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<RoleModel> _roleManager;
        private readonly rpContext _rpContext;

        public AccountRepository(UserManager<UserModel> userManager, RoleManager<RoleModel> roleManager, rpContext rpContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _rpContext = rpContext;
        }

        public string GetNameById(int userId)
        {
            var user = _rpContext.Users.FirstOrDefault(u => u.Id == userId);
            return user.FullName;
        }

        public UserProfileViewModel GetUserInfo(int UserId)
        {
            UserProfileViewModel info = _rpContext.TbUserInformation
                    .Where(n => n.UserId == UserId) // افتراض وجود userId
                    .Select(n => new UserProfileViewModel
                    {
                        FullName = n.FullName,
                        ImagePath = n.ImagePath,
                        Address = n.Address,
                        CvPath = n.CvPath,
                        Skills = n.Skills
                    }).FirstOrDefault();
            info.Email = _rpContext.TbUser.Where(n => n.Id == UserId).Select(n => n.Email).FirstOrDefault();
            info.AppliedOpportunitiesCount = _rpContext.TbOffersForOpportunities.Where(n => n.UserId ==  UserId).Count();
            info.AcceptedOpportunitiesCount = _rpContext.TbOffersForOpportunities.Where(n => n.UserId == UserId && n.Status == "Accept").Count();
            return info;
        }
    }
}
