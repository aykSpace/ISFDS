using System;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class PredictTaskViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PredictTaskViewModel_IdNu_RequiredMessage")]
        [Display(Name = "Идентификатор НУ")]
        [Range(1, long.MaxValue, ErrorMessage = "идентификатор должен быть больше 0!")]
        public int IdNu { get; set; }

        [Display(Name = "Виток")]
        [Range(1, long.MaxValue, ErrorMessage = "виток должен быть больше 0!")]
        public int Circle { get; set; }

        [Display(Name = "Дата и время прогноза")]
        public DateTime DateTimePredict { get; set; }

        [Display(Name = "Аргумент широты прогноза (град)")]
        [Range(0, 360, ErrorMessage = "аргумент широты изменяется от 0 до 360 градусов")]
        public int U { get; set; }

        [Display(Name = "График изменения наклонения")]
        public bool GrafICircle { get; set; }

        [Display(Name = "График изменения большой полуоси")]
        public bool GrafUt { get; set; }

        [Display(Name = "График изменения ....")]
        public bool Graf { get; set; }
    }
}