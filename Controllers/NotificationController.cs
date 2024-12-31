using Microsoft.AspNetCore.Mvc;
using Recruitment_Platform.Models;
using Recruitment_Platform.Repository;

namespace Recruitment_Platform.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationController(INotificationRepository notificationRepository)
        { _notificationRepository = notificationRepository; }
        public IActionResult AllNotifications()
        {
            int UserId = HttpContext.Session.GetInt32("UserId").Value;
            List<NotificationModel> Notifications = _notificationRepository.GetAllForUser(UserId);
            return View(Notifications);
        }
    }
}
