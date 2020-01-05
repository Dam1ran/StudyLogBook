using System.ComponentModel.DataAnnotations;

namespace NutshellRepo.ViewModels.Account
{
    public class AccountRegisterViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name cannot be less than 3 characters.")]
        [MaxLength(50, ErrorMessage = "User Name cannot be greater than 50 characters.")]
        [Display(Name = "User Name:")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Compare("Email")]
        [Display(Name = "Confirm Email:")]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password"/*,ErrorMessage ="Password and Confirmation Password does not match"*/)]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }

    }
}
