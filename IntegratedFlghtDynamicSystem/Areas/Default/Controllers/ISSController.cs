using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BalPredictCLR;
using IntegratedFlghtDynamicSystem.Areas.Default.Models;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using IntegratedFlghtDynamicSystem.Filters;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.Grid;
using OrbitElementsCalc;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Controllers
{
    // ReSharper disable once InconsistentNaming
    [Authorize]
    public class ISSController : SpacecraftBaseController
    {
        internal override sealed void InitializeViewModel()
        {
            MainSpacecraftLayoutViewModel = new MainSpacecraftLayoutViewModel {CotrollerName = "ISS"};
            ViewData["MainSpacecraftLayoutViewModel"] = MainSpacecraftLayoutViewModel;
        }

        public ISSController()
        {
            InitializeViewModel();
        }


        public ActionResult Test()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}