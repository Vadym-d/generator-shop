﻿using System.ComponentModel.DataAnnotations;

namespace GeneratorShop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmedPassword { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        public string ReturnUrl { get; set; }
    }
}
