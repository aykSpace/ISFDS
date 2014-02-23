using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Models;

namespace IntegratedFlghtDynamicSystem.Mappers
{
    public class MassInerCharactMapper : IMapper
    {

        static MassInerCharactMapper()
        {
            Mapper.CreateMap<MassInertialCharacteristic, MassInertialCharactViewModel>();
            Mapper.CreateMap<MassInertialCharactViewModel, MassInertialCharacteristic>();
        }
        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}