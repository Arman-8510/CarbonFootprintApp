﻿using System.ComponentModel.DataAnnotations;

namespace CarbonFootprintApp.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please, provied valid email address.")]
        [StringLength(100, MinimumLength = 5)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, provied your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me.")]
        public bool IsRememberMe { get; set; }
    }
}