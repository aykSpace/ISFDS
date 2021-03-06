﻿using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Mappers;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using Ninject;

namespace IntegratedFlghtDynamicSystem.Controllers
{
    public abstract class BaseController : Controller
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        [Inject]
        public IMapper SpaceCraftModelMapper { get; set; }

        private IMapper _massInerCharactMapper;

        public IMapper MassInerCharactMapper
        {
            get
            {
                return _massInerCharactMapper ?? new MassInerCharactMapper();
            }
            set
            {
                _massInerCharactMapper = value;
            }
        }

        private IMapper _engineMapper;

        public IMapper EngineMapper
        {
            get
            {
                return _engineMapper ?? new EngineMapper();
            }
            set
            {
                _engineMapper = value;
            }
        }


        public static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


    }
}
