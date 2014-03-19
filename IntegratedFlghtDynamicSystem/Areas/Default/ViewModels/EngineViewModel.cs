using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class EngineViewModel
    {
        [Display(Name = "Идентификатор двигателя")]
        public int ID_Engine { get; set; }

        [Display(Name = "Имя двигателя")]
        public string NameEngine { get; set; }

        [Display(Name = "Тяга")]
        public float Trust { get; set; }

        [Display(Name = "Удельный импульс")]
        public float SpecificImpulse { get; set; }

        [Display(Name = "Запас топлива")]
        public int? FuelAmount { get; set; }

        [Display(Name = "Максимальное время работы")]
        public float? MaxTimeOfWorking   { get; set; }

        [Display(Name = "Тип двигателя")]
        public string TypeOfEngine { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
    }
}