using System.ComponentModel.DataAnnotations;

namespace GeneratorShop.Models
{
    public class EditUserViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Пошта обов'язкова!")]
        [EmailAddress(ErrorMessage = "Не правильнгий формат адреси")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Телефон обов'язковий!")]
        [Phone(ErrorMessage = "Не правильний формат телефону")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пароль обов'язковий!")]
        [MinLength(8, ErrorMessage = "Пароль не може бути менший за 8 символів!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Новий пароль обов'язковий!")]
        [MinLength(8, ErrorMessage = "Пароль не може бути менший за 8 символів!")]
        public string NewPassword { get; set; }
    }
}
