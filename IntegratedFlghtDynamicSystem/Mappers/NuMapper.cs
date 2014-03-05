using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Models;

namespace IntegratedFlghtDynamicSystem.Mappers
{
    public class NuMapper : IMapper
    {
        static NuMapper()
        {
            Mapper.CreateMap<NU, NuViewModel>();
            Mapper.CreateMap<NuViewModel, NU>();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}