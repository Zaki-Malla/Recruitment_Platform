using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;

namespace Recruitment_Platform.Repository
{
    public interface INotificationRepository
    {
        List<NotificationModel> GetAllForUser(int UserId);
        List<NotificationModel> GetLastFour(int UserId);
        void AddNotification(NotificationModel notification);

    }
}
