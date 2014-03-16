using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;

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