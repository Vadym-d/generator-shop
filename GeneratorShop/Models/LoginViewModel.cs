using System.ComponentModel.DataAnnotations;

namespace GeneratorShop.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Пошта обов'язкова!")]
        [EmailAddress(ErrorMessage = "Не правильнгий формат адреси")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль обов'язковий!")]
        [MinLength(8, ErrorMessage = "Пароль не може бути менший за 8 символів!")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
