using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using IntegratedFlghtDynamicSystem.Models;
using PagedList;
using Resources;

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
        [ActionName("AddEngine")]
        public PartialViewResult AddEngineForm(int? id)
        {
            if (id == null)
            {
                return PartialView();
            }
            var engine = UnitOfWork.EngineRepository.GetById(id);
            EngineViewModel engineVm;
            if (engine != null)
            {
                engineVm = (EngineViewModel)EngineMapper.Map(engine, typeof(Engine),
                typeof(EngineViewModel));
            }
            else
            {
                ModelState.AddModelError("", Resource.Engine_not_found);
                engineVm = new EngineViewModel();
            }
            return PartialView(engineVm);
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
                UnitOfWork.Save();
                if (Session["SpCrId"] != null)
                {
                    int idSpcr = Convert.ToInt32(Session["SpCrId"]);
                    UnitOfWork.SpacecraftEnginesRepository.Insert(new SpacecraftsEngines
                    {
                        SpacecraftInitDataId = idSpcr,
                        EngineId = engine.ID_Engine
                    });
                    UnitOfWork.Save();
                }
                ViewBag.Success = "Engine was added!";
                
            }
            catch (DataException exception)
            {
                ViewBag.Error = Resource.DatabaseError;
                Logger.Error(exception.Message);
                return View();
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);
                ViewBag.Error = Resource.UnexpectedError;
                return View();
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { Message = ViewBag.Success });
            }
            return View();
        }


        /// <summary>
        /// Редактирование двигателя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult EditEngine(int id)
        {
            var engine = UnitOfWork.EngineRepository.GetById(id);
            EngineViewModel engineVm;
            if (engine != null)
            {
                engineVm = (EngineViewModel)EngineMapper.Map(engine, typeof(Engine),
                typeof(EngineViewModel));
            }
            else
            {
                ModelState.AddModelError("", Resource.Engine_not_found);
                engineVm = new EngineViewModel();
            }
            return PartialView(engineVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEngine(int id, EngineViewModel engineViewModel)
        {
            try
            {
                if (ModelState.IsValid && engineViewModel.ID_Engine == id)
                {
                    var engine =
                   (Engine)
                       EngineMapper.Map(engineViewModel, typeof(EngineViewModel),
                           typeof(Engine));
                    UnitOfWork.EngineRepository.Update(engine);
                    UnitOfWork.Save();
                    ViewBag.Success = "Engine was updated!";
                }
            }

            catch (DbUpdateConcurrencyException exception)
            {
                ViewBag.Error = Resource.DatabaseError;
                Logger.Error(exception.Message);
                throw new HttpRequestException("request exception");
            }
            catch (Exception exception)
            {

                Logger.Error(exception.Message);
                ViewBag.Error = Resource.UnexpectedError;
                throw new HttpRequestException("request exception");
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { Message = ViewBag.Success});   
            }
            return View();
        }


        /// <summary>
        /// Удаление двигателя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult DeleteEngine(int id)
        {
            var engine = UnitOfWork.EngineRepository.GetById(id);
            EngineViewModel engineVm;
            if (engine != null)
            {
                engineVm = (EngineViewModel)EngineMapper.Map(engine, typeof(Engine),
                typeof(EngineViewModel));
            }
            else
            {
                ModelState.AddModelError("", Resource.Engine_not_found);
                engineVm = new EngineViewModel();
            }
            return PartialView(engineVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEngine(int id, EngineViewModel engineViewModel)
        {
            try
            {
                if (id > 0)
                {
                    UnitOfWork.EngineRepository.Delete(id);
                    UnitOfWork.Save();
                    ViewBag.Success = "Engine was deleted";
                }
            }

            catch (DbUpdateConcurrencyException exception)
            {
                ViewBag.Error = Resource.DatabaseError;
                Logger.Error(exception.Message);
                throw new HttpRequestException("request exception");
            }
            catch (Exception exception)
            {

                Logger.Error(exception.Message);
                ViewBag.Error = Resource.UnexpectedError;
                throw new HttpRequestException("request exception");
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { Message = ViewBag.Success });
            }
            return View();
        }


    }
}
