using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Recruitment_Platform.Models.HelperModels
{
    public class CompleteProfileViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; } 

        public string FullName { get; set; } 

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } 

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } 
        [Required(ErrorMessage = "Skills are required.")]
        public string Skills { get; set; } 

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; } 

        [StringLength(500)]
        public string Bio { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        public IFormFile Cv { get; set; }

        public IFormFile Image { get; set; }

    }
}
