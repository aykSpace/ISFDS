using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Data.Edm.Expressions;

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

        private List<int> _avalibleMicId = new List<int>();

        public List<int> AvalibleMicId
        {
            get { return _avalibleMicId; }
               
        }



        public IEnumerable<SelectListItem> AvalibleMicIdSelectListItems
        {
            get
            {
                return AvalibleMicId.Select((t, i) => new SelectListItem
                {
                    Value = i.ToString(CultureInfo.InvariantCulture),
                    Text = t.ToString(CultureInfo.InvariantCulture),
                    Selected = t == i
                });
            }
        } 

    }
}