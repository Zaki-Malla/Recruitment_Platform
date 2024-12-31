using Recruitment_Platform.Models.HelperModels;

namespace Recruitment_Platform.Repository
{
    public interface IAccountRepository
    {
        string GetNameById(int userId);
        UserProfileViewModel GetUserInfo(int UserId);
    }
}
