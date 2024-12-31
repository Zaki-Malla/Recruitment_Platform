using Recruitment_Platform.Models;
using System.Linq;

namespace Recruitment_Platform.Repository
{
    public class InformationRepository : IInformationRepository
    {
        private readonly rpContext _rpContext;
        public InformationRepository(rpContext rpContext)
        {
            _rpContext = rpContext;
        }
        public void NewRow(UserInformationModel userInformation)
        {
            _rpContext.TbUserInformation.Add(userInformation);
            _rpContext.SaveChanges();
        }
        public string GetName(int UserId)
        {
            UserModel user = _rpContext.TbUser.Where(x => x.Id == UserId).FirstOrDefault();
            return user.FullName;
        }
        public string GetEmail(int UserId)
        {
            UserModel user = _rpContext.TbUser.Where(x => x.Id == UserId).FirstOrDefault();
            return user.Email;
        }
        public void UpdateInformation(UserInformationModel userInformation)
        {
            UserInformationModel OldInfo = _rpContext.TbUserInformation.FirstOrDefault(x => x.UserId == userInformation.UserId);
            OldInfo.Address = userInformation.Address;
            OldInfo.CvPath = userInformation.CvPath;
            OldInfo.ImagePath = userInformation.ImagePath;
            OldInfo.Skills = userInformation.Skills;
            OldInfo.Age = userInformation.Age;
            _rpContext.Update(OldInfo);
            _rpContext.SaveChanges();
        }

        public UserInformationModel GetUserInformation(int UserId)
        {
            return _rpContext.TbUserInformation.FirstOrDefault(x => x.UserId == UserId);
        }
    }
}
