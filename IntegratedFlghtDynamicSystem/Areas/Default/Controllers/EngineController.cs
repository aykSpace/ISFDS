using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using IntegratedFlghtDynamicSystem.Models;
using PagedList;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Controllers
{
    public class EngineController : BaseController
    {
        //
        // GET: /Default/Engine/2
        /// <summary>
        /// Возвращает список двигателей постранично
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PartialViewResult GetEngines(int? page)
        {
            int pageNumber = page ?? 1;
            const int pageSize = 5;
            var engines = UnitOfWork.EngineRepository.Get();
            var enginesVm =
                engines.Select(
                    m =>
                        (EngineViewModel)
                            EngineMapper.Map(m, typeof(Engine), typeof(EngineViewModel)));
            return PartialView(enginesVm.ToPagedList(pageNumber, pageSize));
        }


        /// <summary>
        /// Добавить новый двигатель
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult AddEngine()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEngine(EngineViewModel engineViewModel)
        {
            try
            {
                var engine =
                    (Engine)
                        EngineMapper.Map(engineViewModel, typeof(EngineViewModel),
                            typeof(Engine));
                UnitOfWork.EngineRepository.Insert(engine);
                if (Session["SpCrId"] != null)
                {
                    int idSpcr = Convert.ToInt32(Session["SpCrId"]);
                    UnitOfWork.SpacecraftCommonDataRepository.Insert(new SpaceсraftCommonData
                    {
                        SpacecraftInitDataId = idSpcr
                    });
                }
                ViewBag.Success = "Engine was added!";
                UnitOfWork.Save();
            }
            catch (DataException exception)
            {
                ViewBag.Error = Resources.Resource.DatabaseError;
                Logger.Error(exception.Message);
                throw new HttpException(ViewBag.Error);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);
                ViewBag.Error = Resources.Resource.UnexpectedError;
                throw new HttpException(ViewBag.Error);
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { Message = ViewBag.Success });
            }
            return View();
        }


    }
}
