﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ISFDS2Entities : DbContext
    {
        public ISFDS2Entities()
            : base("name=ISFDS2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<NU> NUs { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<MassInertialCharacteristic> MassInertialCharacteristics { get; set; }
        public DbSet<SpacecraftInitialData> SpacecraftInitialDatas { get; set; }
        public DbSet<SpacecraftsEngines> SpacecraftsEngines { get; set; }
        public DbSet<SpaceсraftCommonData> SpaceсraftCommonData { get; set; }
    }
}
