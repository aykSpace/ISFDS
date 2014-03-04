using System.ComponentModel.DataAnnotations;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class SpacecraftViewModel
    {
        [Required]
        [Display(Name = "Номер КА")]
        public int SpacecraftNumber { get; set; }

        [Required]
        [Display(Name = "Название КА")]
        public string SpacecraftName { get; set; }

        [Required]
        [Display(Name = "Международный номер КА")]
        public string InternationalNumber { get; set; }

        [Required]
        [Display(Name = "Тип КА")]
        public string SpacecraftType { get; set; }

        [Required]
        [Display(Name = "Тип орбиты")]
        public string OrbitType { get; set; }

        [Required]
        [Display(Name = "Ракета выведения")]
        public string Launcher { get; set; }

        [Display(Name = "Дата старта")]
        public string DateOfLaunch { get; set; }

        [Required]
        [Display(Name = "Идентификатор МИХ")]
        public int MassInerCharacteristicId  { get; set; }

        [Display(Name = "Комментарии")]
        public string Comments { get; set; }
    }
}