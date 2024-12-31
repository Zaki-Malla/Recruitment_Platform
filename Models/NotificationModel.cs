using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment_Platform.Models
{
    public class NotificationModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
    }
}
