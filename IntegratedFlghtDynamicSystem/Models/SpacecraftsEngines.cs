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
    
    public partial class SpacecraftsEngines
    {
        public int SpacecragtsEngineId { get; set; }
        public int SpacecraftInitDataId { get; set; }
        public int EngineId { get; set; }
    
        public virtual Engine Engine { get; set; }
        public virtual SpacecraftInitialData SpacecraftInitialData { get; set; }
    }
}
