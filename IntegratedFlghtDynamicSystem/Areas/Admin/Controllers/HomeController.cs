<<<<<<< HEAD
﻿using System.Data;
using System.Linq;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
>>>>>>> Admincontroller
using System.Web.Mvc;
using AutoMapper;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using IntegratedFlghtDynamicSystem.Models;
<<<<<<< HEAD
using PagedList;
=======
>>>>>>> Admincontroller

namespace IntegratedFlghtDynamicSystem.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Admin/Home/
<<<<<<< HEAD
        /// <summary>
        /// Main page of admin area with list of servicing spacecrafts
        /// </summary>
        /// <returns>main page</returns>
=======

>>>>>>> Admincontroller
        public ActionResult Index()
        {
            var spacecrafts = UnitOfWork.SpacecraftInfoRepository.Get();

            var spcrVm =
                spacecrafts.Select(
                    spcr => (SpacecraftViewModel)Mapper.Map(spcr, typeof(SpacecraftInitialData), typeof(SpacecraftViewModel))).ToList();
            return View(spcrVm);
        }

<<<<<<< HEAD

        /// <summary>
        /// Add spacecraft initial data and automatically spacecraftCommonData 
        /// </summary>
        /// <returns></returns>
=======
>>>>>>> Admincontroller
        public ActionResult AddSpacecraft()
        {
            return View();
        }

<<<<<<< HEAD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpacecraft(SpacecraftViewModel spacecraftViewModel)
        {
            var spcraft = (SpacecraftInitialData)Mapper.Map(spacecraftViewModel, typeof(SpacecraftViewModel),
                    typeof(SpacecraftInitialData));
            UnitOfWork.SpacecraftInfoRepository.Insert(spcraft);
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

        public PartialViewResult GetAvaliableMic(int? page)
        {
            int pageNumber = page ?? 1;
            const int pageSize = 5;
            var mics = UnitOfWork.MicRepository.Get();
            var micVMs =
                mics.Select(
                    m =>
                        (MassInertialCharactViewModel)
                            MassInerCharactMapper.Map(m, typeof (MassInertialCharacteristic),
                                typeof (MassInertialCharactViewModel)));
            return PartialView(micVMs.ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult GetAvaliableEngines()
        {
            var engines = UnitOfWork.EngineRepository.Get();
            var enginesVm =
                engines.Select(
                    m =>
                        (EngineViewModel)
                            EngineMapper.Map(m, typeof(Engine), typeof(EngineViewModel)));
            return PartialView(enginesVm);
        }

        public ActionResult ShowAddMicForm()
        {
            return View();
        }


=======
>>>>>>> Admincontroller
    }
}
