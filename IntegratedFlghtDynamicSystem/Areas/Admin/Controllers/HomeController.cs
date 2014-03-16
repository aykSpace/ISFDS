using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult Index()
        {
            var spacecrafts = UnitOfWork.SpacecraftInfoRepository.Get();

            var spcrVm =
                spacecrafts.Select(
                    spcr => (SpacecraftViewModel)Mapper.Map(spcr, typeof(SpacecraftInitialData), typeof(SpacecraftViewModel))).ToList();
            return View(spcrVm);
        }

        public ActionResult AddSpacecraft()
        {
            return View();
        }

    }
}
