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
    public class ISSController : BaseController
    {
        /// <summary>
        ///     The flag used to notify that the view was created from Index method.
        /// </summary>
        private static bool _fromIndex;

        public List<DiagramData> DiagramDatas { get; set; }

        //
        // GET: /Default/ISS/
        [HttpGet]
        public ActionResult Index()
        {
            //Session["SpCrId"] = id;
            //_fromIndex = true;
            return View();
        }

        public PartialViewResult Search()
        {
            string startDate = Request["from"];
            string endDate = Request["to"];

            const string format = "MM/dd/yyyy";
            var provider = CultureInfo.InvariantCulture;
            // Cache the start and end dates into the session to be used by later one in the view.
            Session["StartDate"] = (startDate.Length < 1
                ? null
                : (object)DateTime.ParseExact(startDate, format, provider));
            Session["EndDate"] = (endDate.Length < 1
                ? null
                : (object)DateTime.ParseExact(endDate, format, provider));

            return PartialView("_NuGrid");
        }

        public JsonResult GetData(GridSettings grid)
        {
            if (_fromIndex && Session["NuGridSettings"] != null)
            {
                //
                // Get grid settings from cache!
                //
                grid = new GridSettings((string)Session["NuGridSettings"]);
            }

            if (Session["SpCrId"] != null)
            {
                var spCrIntiDataID  = (int)Session["SpCrId"];
                _fromIndex = false; // Must be set on false here!
                //
                // Load the data from the database for the current grid settings.
                //
                DateTime searchStartDate = (Session["StartDate"] == null
                    ? DateTime.MinValue
                    : (DateTime)Session["StartDate"]);
                DateTime searchEndDate = (Session["EndDate"] == null ? DateTime.MinValue : (DateTime)Session["EndDate"]);
                int count;
                IQueryable<NU> query = grid.LoadGridData(NU.SearchNuByDates(UnitOfWork, searchStartDate, searchEndDate, spCrIntiDataID),
                    out count);
                NU[] data = query.ToArray();
                //
                // Convert the results in grid format.
                //
                string gridSettingsString = grid.ToString(); // Used to preserve grid settings!
                Session["NuSettings"] = gridSettingsString;
                gridSettingsString = null;
                var result = new
                {
                    total = (int)Math.Ceiling((double)count / grid.PageSize),
                    page = grid.PageIndex,
                    records = count,
                    rows = (from host in data
                            select new
                            {
                                host.ID_NU,
                                host.N_NU,
                                host.Modification,
                                host.Vitok,
                                host.X,
                                host.Y,
                                host.Z,
                                DateTime =
                                    String.Format("{0} {1}", host.DateNU.ToLongDateString(), host.DateNU.ToLongTimeString()),
                                Checked = false,
                            }).ToArray()
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(new { ID_NU = 0, N_NU = 0, Modification = 0, Vitok = 0, X = 0, Y = 0, Z = 0, DateTime = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Predict()
        {
            return View();
        }

        [HttpPost]
        [AcceptAjax]
        [AjaxAutorize]
        public async Task<PartialViewResult> Predict(PredictTaskViewModel predictTaskViewModel)
        {
            var factoryPredict = new FactoryPredict(predictTaskViewModel, UnitOfWork);
            var client = new Client(factoryPredict);
            try
            {
                DiagramDatas = await client.RunAsync();
                ViewBag.OperationName = "Операция прогноза положения КА завершилась успешно!";
                if (DiagramDatas.Count > 0)
                {
                    TempData["grafData"] = DiagramDatas;
                    if (predictTaskViewModel.GrafICircle)
                    {
                        Session["GrafI"] = true;
                    }
                    if (predictTaskViewModel.GrafUt)
                    {
                        Session["GrafU"] = true;
                    }
                    else if (predictTaskViewModel.GrafICircle && predictTaskViewModel.GrafUt)
                    {
                        Session["GrafI"] = Session["GrafU"] = true;
                    }
                }
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.Message;
            }


            return PartialView("_PredictResult");
        }


        public PartialViewResult CalculateOrbitElements(int id)
        {
            var nu = UnitOfWork.NuRepository.GetById(id);
            var vector = new BalVector((uint)nu.Vitok, nu.t, nu.X, nu.Y, nu.Z, nu.VX, nu.VY, nu.VZ, nu.Sbal,
                nu.DateNU);
            BalVector.ap = 110;
            BalVector.f = 16;
            var orbElements = new OrbitElementsCLI(vector);
            orbElements.GetElements();

            return PartialView(orbElements);
        }

        public ActionResult Charts()
        {
            ViewBag.ErrorChart = "Нет данных для отображения!";
            return View();
        }

        public ActionResult GetChartsData(int id)
        {
            var data = TempData["grafData"] as List<DiagramData>;
            if (data == null)
            {
                ViewBag.ErrorChart = "Нет данных для построения графиков!";
                return RedirectToAction("Predict");
            }
            if (data.Count > 1)
            {
                TempData["grafData"] = data;
            }
            return Json(data[id].Points, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Default/ISS/ISSInitialData/id

        public ActionResult ISSInitialData(int id)
        {
            Session["SpCrId"] = id;
            _fromIndex = true;
            var spaceCraftInitData = UnitOfWork.SpacecraftInfoRepository.GetById(id);
            var spCrViewModel = SpaceCraftModelMapper.Map(spaceCraftInitData, typeof (SpacecraftInitialData),
                typeof (SpacecraftViewModel));
            return View(spCrViewModel);
        }


        public PartialViewResult MassInertialCharachteristic(int idCharacteristic)
        {
            var mic = UnitOfWork.MicRepository.GetById(idCharacteristic);
            var micVm = (MassInertialCharactViewModel)MassInerCharactMapper.Map(mic, typeof (MassInertialCharacteristic),
                typeof (MassInertialCharactViewModel));
            int idSpcr = Convert.ToInt32(Session["SpCrId"]);
            var listOfCommonDatas = UnitOfWork.SpacecraftCommonDataRepository.Get().Where(x => x.SpacecraftInitDataId == idSpcr);
            foreach (var spaceсraftCommonData in listOfCommonDatas)
            {
                micVm.AvalibleMicId.Add(spaceсraftCommonData.MIC_Id);
            }
            
            return PartialView(micVm);
        } 

        public ActionResult Test()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}