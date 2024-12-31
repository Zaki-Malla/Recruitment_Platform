using Microsoft.EntityFrameworkCore;
using Recruitment_Platform.Models;
using Recruitment_Platform.Models.HelperModels;

namespace Recruitment_Platform.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly rpContext _rpContext;
        public NotificationRepository(rpContext rpContext) { _rpContext = rpContext; }
        public List<NotificationModel> GetAllForUser(int UserId)
        {
            List<NotificationModel> Notifis = _rpContext.TbNotifications
                    .Where(n => n.UserId == UserId) 
                    .OrderByDescending(n => n.Id)
                    .ToList();
            return Notifis;
        }
        public List<NotificationModel> GetLastFour(int UserId)
        {
            List<NotificationModel> Notifis = _rpContext.TbNotifications
                    .Where(n => n.UserId == UserId) 
                    .OrderByDescending(n => n.Id)
                    .Take(4)
                    .ToList();
            return Notifis;
        }
        public void AddNotification(NotificationModel notification)
        {
            _rpContext.TbNotifications.Add(notification);
            _rpContext.SaveChanges();
        }

    }
}
