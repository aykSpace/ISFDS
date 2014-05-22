using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Controllers
{
    // ReSharper disable once InconsistentNaming
    [Authorize]
    public class CanopusController : SpacecraftBaseController
    {
        internal override sealed void InitializeViewModel()
        {
            MainSpacecraftLayoutViewModel = new MainSpacecraftLayoutViewModel
            {
                CotrollerName = "Canopus",
                Id = 3
            };
            ViewData["MainSpacecraftLayoutViewModel"] = MainSpacecraftLayoutViewModel;
        }

        public CanopusController()
        {
            InitializeViewModel();
        }
    }
}