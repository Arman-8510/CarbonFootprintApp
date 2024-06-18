using System.ComponentModel.DataAnnotations;

namespace CarbonFootprintApp.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please, provied first name.")]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, provied last name.")]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please, provide valid email address.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, provide strong password.")]
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please, provide confirm password.")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password did not match!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please, provied city.")]
        [StringLength(20, MinimumLength = 2)]
        public string City { get; set; }
    }
}