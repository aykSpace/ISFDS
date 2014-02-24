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
    
    public partial class MassInertialCharacteristic
    {
        public MassInertialCharacteristic()
        {
            this.SpacecraftInitialDatas = new HashSet<SpacecraftInitialData>();
            this.SpaceсraftCommonData = new HashSet<SpaceсraftCommonData>();
        }
    
        public int ID_MIC { get; set; }
        public int ID_Aero { get; set; }
        public double Mass { get; set; }
        public Nullable<double> MassOfCommonBay { get; set; }
        public Nullable<double> MassOfDeorbitSpCr { get; set; }
        public Nullable<double> Sbal { get; set; }
        public Nullable<double> XT { get; set; }
        public Nullable<double> YT { get; set; }
        public Nullable<double> ZT { get; set; }
        public string Liter { get; set; }
        public System.DateTime DateOfID { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<SpacecraftInitialData> SpacecraftInitialDatas { get; set; }
        public virtual ICollection<SpaceсraftCommonData> SpaceсraftCommonData { get; set; }
    }
}
