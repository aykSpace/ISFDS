using System.ComponentModel.DataAnnotations;
using IntegratedFlghtDynamicSystem.Filters;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class LoginViewModel
    {
        [ValidEmail(ErrorMessage = "Не корректная почта")]
        [Required(ErrorMessage = "Введите почту")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}