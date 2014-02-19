using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using Ninject;

namespace IntegratedFlghtDynamicSystem.Controllers
{
    public abstract class BaseController : Controller
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        //private UnitOfWork _unitOfWork;
        //public UnitOfWork UnitOfWork
        //{
        //    get { return _unitOfWork ?? (_unitOfWork = new UnitOfWork()); }
        //}

    }
}
