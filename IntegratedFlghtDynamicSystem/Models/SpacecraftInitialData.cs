//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegratedFlghtDynamicSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SpacecraftInitialData
    {
        public SpacecraftInitialData()
        {
            this.NUs = new HashSet<NU>();
        }
    
        public int SpacecraftInitDataId { get; set; }
        public int SpacecraftNumber { get; set; }
        public string InternationalNumber { get; set; }
        public Nullable<int> CCSANumber { get; set; }
        public Nullable<int> NORADNumber { get; set; }
        public string SpacecraftName { get; set; }
        public string SpacecraftType { get; set; }
        public string OrbitType { get; set; }
        public string ReboostBlockType { get; set; }
        public string Launcher { get; set; }
        public Nullable<System.DateTime> DateOfLaunch { get; set; }
        public int MassInerCharacteristicId { get; set; }
        public int EngineID { get; set; }
        public string Comments { get; set; }
    
        public virtual ICollection<NU> NUs { get; set; }
        public virtual Engine Engine { get; set; }
        public virtual MassInertialCharacteristic MassInertialCharacteristic { get; set; }
    }
}
