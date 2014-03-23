using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        private List<int> _avalibleEngineId = new List<int>();

        public List<int> AvalibleEnginecId
        {
            get { return _avalibleEngineId; }

        }
        [Display (Name = "Досупные двигатели")]
        public IEnumerable<SelectListItem> AvalibleMicIdSelectListItems
        {
            get
            {
                return AvalibleEnginecId.Select((t, i) => new SelectListItem
                {
                    Value = i.ToString(CultureInfo.InvariantCulture),
                    Text = t.ToString(CultureInfo.InvariantCulture),
                    Selected = ID_Engine == t
                });
            }
        } 
    }
}