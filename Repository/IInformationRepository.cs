using Recruitment_Platform.Models;

namespace Recruitment_Platform.Repository
{
    public interface IInformationRepository
    {
        void NewRow(UserInformationModel userInformation);
        string GetName(int UserId);
        string GetEmail(int UserId);
        void UpdateInformation(UserInformationModel userInformation);
        UserInformationModel GetUserInformation(int UserId);
    }
}
