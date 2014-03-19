using System;
using System.ComponentModel.DataAnnotations;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class SpacecraftViewModel
    {
        
        [Display(Name = "Идентификатор КА")]
        public int SpacecraftInitDataId { get; set; }

        [Display(Name = "Номер КА")]
        public int SpacecraftNumber { get; set; }

        [Required]
        [Display(Name = "Название КА")]
        public string SpacecraftName { get; set; }

        [Display(Name = "Международный номер КА")]
        public string InternationalNumber { get; set; }

        [Display(Name = "Номер ЦККП")]
        public int? CCSANumber { get; set; }

        [Display(Name = "Номер NORAD")]
        public int? NORADNumber { get; set; }

        [Required]
        [Display(Name = "Тип КА")]
        public string SpacecraftType { get; set; }

        [Required]
        [Display(Name = "Тип орбиты")]
        public string OrbitType { get; set; }

        [Display(Name = "Ракета-носитель")]
        public string Launcher { get; set; }

        [Display(Name = "Разгонный блок")]
        public string ReboostBlockType { get; set; }

        [Display(Name = "Дата старта")]
        public DateTime? DateOfLaunch { get; set; }


        [Display(Name = "Идентификатор МИХ")]
        public int MassInerCharacteristicId  { get; set; }

        [Display(Name = "Идентификатор ДУ")]
        public int EngineID { get; set; }

        [Display(Name = "Комментарии")]
        public string Comments { get; set; }
    }
}