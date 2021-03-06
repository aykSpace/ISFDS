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
    
    public partial class Engine
    {
        public Engine()
        {
            this.SpacecraftInitialData = new HashSet<SpacecraftInitialData>();
            this.SpacecraftsEngines = new HashSet<SpacecraftsEngines>();
        }
    
        public int ID_Engine { get; set; }
        public string NameEngine { get; set; }
        public float Trust { get; set; }
        public float SpecificImpulse { get; set; }
        public Nullable<int> FuelAmount { get; set; }
        public Nullable<float> MaxTimeOfWorking { get; set; }
        public string TypeOfEngine { get; set; }
        public string Comment { get; set; }
    
        public virtual ICollection<SpacecraftInitialData> SpacecraftInitialData { get; set; }
        public virtual ICollection<SpacecraftsEngines> SpacecraftsEngines { get; set; }
    }
}
