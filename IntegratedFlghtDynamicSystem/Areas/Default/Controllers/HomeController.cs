﻿using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Controllers;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var test = UnitOfWork;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}