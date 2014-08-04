using System.Data;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using IntegratedFlghtDynamicSystem.Models;

namespace IntegratedFlghtDynamicSystem.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Admin/Home/
        /// <summary>
        /// Main page of admin area with list of servicing spacecrafts
        /// </summary>
        /// <returns>main page</returns>
        public ActionResult Index()
        {
            var spacecrafts = UnitOfWork.SpacecraftInfoRepository.Get();

            var spcrVm =
                spacecrafts.Select(
                    spcr => (SpacecraftViewModel)Mapper.Map(spcr, typeof(SpacecraftInitialData), typeof(SpacecraftViewModel))).ToList();
            return View(spcrVm);
        }


        /// <summary>
        /// Add spacecraft initial data and automatically spacecraftCommonData 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSpacecraft()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpacecraft(SpacecraftViewModel spacecraftViewModel)
        {
            var spcraft = (SpacecraftInitialData)Mapper.Map(spacecraftViewModel, typeof(SpacecraftViewModel),
                    typeof(SpacecraftInitialData));
            UnitOfWork.SpacecraftInfoRepository.Insert(spcraft);
            UnitOfWork.SpacecraftCommonDataRepository.Insert(new SpaceсraftCommonData
            {
                MIC_Id = spcraft.MassInerCharacteristicId,
                SpacecraftInitDataId = spcraft.SpacecraftInitDataId
            });
            UnitOfWork.SpacecraftEnginesRepository.Insert(new SpacecraftsEngines
            {
                SpacecraftInitDataId = spcraft.SpacecraftInitDataId,
                EngineId = spcraft.EngineID
            });
            try
            {
                UnitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (DataException exception)
            {
                ViewBag.Error = Resources.Resource.DatabaseError;
                Logger.Error(exception.Message);
                return View();
            }
        }

        public ActionResult ShowAddMicForm()
        {
            return View();
        }

        public ActionResult ShowAddEngineForm()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult ShowEditForm(int id)
        {
            var spcrVm = (SpacecraftViewModel)Mapper.Map(UnitOfWork.SpacecraftInfoRepository.GetById(id), typeof(SpacecraftInitialData),
                typeof(SpacecraftViewModel));
            return PartialView(spcrVm);
        }

    }
}
