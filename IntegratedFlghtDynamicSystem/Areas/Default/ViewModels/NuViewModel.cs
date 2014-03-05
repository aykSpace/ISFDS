using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class NuViewModel
    {
        [Display (Name = "Идентификатор НУ")]
        [Required]
        public int ID_NU { get; set; }

        [Display(Name = "Номер НУ")]
        [Required]
        public int N_NU { get; set; }

        [Display(Name = "Модификация НУ")]
        [Required]
        public int Modification { get; set; }

        [Display(Name = "Идентификатор КА")]
        [Required]
        public int SpacecraftInitialData_ID { get; set; }

        [Display(Name = "X")]
        [Required]
        public double X { get; set; }

        [Display(Name = "Y")]
        [Required]
        public double Y { get; set; }

        [Display(Name = "Z")]
        [Required]
        public double Z { get; set; }

        [Display(Name = "Vx")]
        [Required]
        public double Vx { get; set; }

        [Display(Name = "Vy")]
        [Required]
        public double Vy { get; set; }

        [Display(Name = "Vz")]
        [Required]
        public double Vz { get; set; }

        [Display(Name = "Время от начала суток")]
        [Required]
        public double t { get; set; }

        [Display(Name = "Номер витка")]
        [Required]
        public int Vitok { get; set; }

        [Display(Name = "Баллистический коэффициент")]
        [Required]
        public double Sbal { get; set; }

        [Display(Name = "Дата НУ")]
        [Required]
        public DateTime DateNU { get; set; }

        [Display(Name = "Комментарий")]
        [Required]
        public string Comment { get; set; }

    }
}