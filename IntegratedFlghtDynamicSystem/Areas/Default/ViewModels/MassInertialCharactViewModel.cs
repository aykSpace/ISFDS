using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class MassInertialCharactViewModel
    {
        [Required]
        [Display(Name = "Идентификатор")]
        public int ID_MIC { get; set; }

        [Required]
        [Display(Name = "Масса КА")]
        public double Mass { get; set; }

        [Required]
        [Display(Name = "Бал. коэффициент")]
        public double Sbal { get; set; }

        [Required]
        [Display(Name = "Центровка X")]
        public double XT { get; set; }

        [Required]
        [Display(Name = "Центровка Y")]
        public double YT { get; set; }

        [Required]
        [Display(Name = "Центровка Z")]
        public double ZT { get; set; }

        [Required]
        [Display(Name = "Дата записи МИХ")]
        public DateTime DateOfID { get; set; }

        [Required]
        [Display(Name = "Литер")]
        public string Liter { get; set; }

        [Required]
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
    }
}