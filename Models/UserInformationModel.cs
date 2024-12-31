using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Recruitment_Platform.Models
{
    public class UserInformationModel
    {

        [Key]
        public int Id { get; set; } 

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserModel User { get; set; }
        public string FullName { get; set; }

        [AllowNull]
        public string? Address { get; set; }

        [AllowNull]
        public List<string> Skills { get; set; } = new List<string>();

        [AllowNull]
        public string? CvPath { get; set; }

        [AllowNull]
        public string? ImagePath { get; set; }

        [AllowNull]
        public int? Age { get; set; }

    }


}
