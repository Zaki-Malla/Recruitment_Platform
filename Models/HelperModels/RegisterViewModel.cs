using System.ComponentModel.DataAnnotations;

namespace Recruitment_Platform.Models.HelperModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select account type")]
        public string AccountType { get; set; } // "User" أو "Employer"

        [Required(ErrorMessage = "You must accept the terms")]
        public bool AcceptTerms { get; set; }
    }
}
