using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
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
using HttpNotFoundResult = System.Web.Mvc.HttpNotFoundResult;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Controllers
{
    public abstract class SpacecraftBaseController : BaseController
    {
        public MainSpacecraftLayoutViewModel MainSpacecraftLayoutViewModel { get; set; }

        /// <summary>
        /// Инициализирует MainSpacecraftLayoutViewModel с конкретными свойствами
        /// </summary>
        internal abstract void InitializeViewModel();

        /// <summary>
        ///     The flag used to notify that the view was created from Index method.
        /// </summary>
        private static bool _fromIndex;

        //
        // GET: /Default/SpacecraftBase/id
        /// <summary>
        /// Главная страница с информацией o КА
        /// </summary>
        /// <param name="id">Id SpacecraftInitialData</param>
        /// <returns></returns>
        public virtual ActionResult Index(int id)
        {
            Session["SpCrId"] = id;
            var spaceCraftInitData = UnitOfWork.SpacecraftInfoRepository.GetById(id);
            if (spaceCraftInitData != null)
            {
                var spCrViewModel = SpaceCraftModelMapper.Map(spaceCraftInitData, typeof(SpacecraftInitialData),
                typeof(SpacecraftViewModel));
                ViewBag.Controller = "Home";
                return View(spCrViewModel);
            }
            return new HttpNotFoundResult("Spacecraft not found");
        }

        /// <summary>
        /// Поиск по начальным условиям
        /// </summary>
        /// <returns>partial NuGrid</returns>
        public  PartialViewResult Search()
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

        /// <summary>
        /// Данные для грида с начальными условиям
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
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
                var spCrIntiDataId = (int)Session["SpCrId"];
                _fromIndex = false; // Must be set on false here!
                //
                // Load the data from the database for the current grid settings.
                //
                DateTime searchStartDate = (Session["StartDate"] == null
                    ? DateTime.MinValue
                    : (DateTime)Session["StartDate"]);
                DateTime searchEndDate = (Session["EndDate"] == null ? DateTime.MinValue : (DateTime)Session["EndDate"]);
                int count;
                IQueryable<NU> query = grid.LoadGridData(NU.SearchNuByDates(UnitOfWork, searchStartDate, searchEndDate, spCrIntiDataId),
                    out count);
                NU[] data = query.ToArray();
                //
                // Convert the results in grid format.
                //
                string gridSettingsString = grid.ToString(); // Used to preserve grid settings!
                Session["NuSettings"] = gridSettingsString;
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

        //
        // GET: /Default/SpacecraftBase/SpacecraftVectorsInitialData
        /// <summary>
        /// Грид с векторами состояния КА
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SpacecraftVectorsInitialData()
        {
            _fromIndex = true;
            return View();
        }

        //
        // GET: /Default/ISS/MassInertialCharachteristic

        /// <summary>
        /// Управление МИХами
        /// </summary>
        /// <returns></returns>
        public virtual PartialViewResult MassInertialCharachteristic()
        {
            int idSpcr = Convert.ToInt32(Session["SpCrId"]);
            var micVm = new MassInertialCharactViewModel();
            var spCr = UnitOfWork.SpacecraftInfoRepository.GetById(idSpcr);
            micVm.ID_MIC = spCr.MassInerCharacteristicId;
            var listOfCommonDatas = UnitOfWork.SpacecraftCommonDataRepository.Get().Where(x => x.SpacecraftInitDataId == idSpcr);
            foreach (var spaceсraftCommonData in listOfCommonDatas)
            {
                micVm.AvalibleMicId.Add(spaceсraftCommonData.MIC_Id);
            }
            Session["MicId"] = UnitOfWork.SpacecraftInfoRepository.GetById(idSpcr).MassInerCharacteristicId;

            return PartialView(micVm);
        }

        /// <summary>
        /// Возвращает выбранные по идентификатору МИХ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json</returns>
        public ActionResult GetMic(int id)
        {
            var mic = UnitOfWork.MicRepository.GetById(id);
            if (mic != null)
            {
                var micVm = (MassInertialCharactViewModel)MassInerCharactMapper.Map(mic, typeof(MassInertialCharacteristic),
                typeof(MassInertialCharactViewModel));
                return Json(micVm, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound("Mass inertial characteristic not found");
        }


        /// <summary>
        /// Задает идентификатор текущих МИХ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RedirectToRouteResult SetCurrentMic(int? id)
        {
            int idSpcr = Convert.ToInt32(Session["SpCrId"]);
            if (id == null)
            {
                return RedirectToAction("Index", new { id = idSpcr });
            }
            var spCr = UnitOfWork.SpacecraftInfoRepository.GetById(idSpcr);
            spCr.MassInerCharacteristicId = Convert.ToInt32(id);
            UnitOfWork.SpacecraftInfoRepository.Update(spCr);
            UnitOfWork.Save();
            return RedirectToAction("Index", new { id = idSpcr });
        }

        /// <summary>
        /// Добавить новые МИХ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddMic()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMic(MassInertialCharactViewModel micViewModel)
        {
            var dateMic = Request["DateOfID"] ?? micViewModel.DateOfID.ToString("d", CultureInfo.InvariantCulture);
            const string format = "MM/dd/yyyy";
            var provider = CultureInfo.InvariantCulture;
            micViewModel.DateOfID = DateTime.ParseExact(dateMic, format, provider);
            int idSpcr = Convert.ToInt32(Session["SpCrId"]);
            try
            {
                var mic = (MassInertialCharacteristic)MassInerCharactMapper.Map(micViewModel, typeof(MassInertialCharactViewModel),
                    typeof(MassInertialCharacteristic));
                UnitOfWork.MicRepository.Insert(mic);

                var currentEngineId = UnitOfWork.SpacecraftInfoRepository.GetById(idSpcr).EngineID;

                UnitOfWork.SpacecraftCommonDataRepository.Insert(new SpaceсraftCommonData
                {
                    SpacecraftInitDataId = idSpcr,
                    EngineId = currentEngineId,
                    MIC_Id = mic.ID_MIC
                });
                UnitOfWork.Save();
            }
            catch (DataException exception)
            {
                ViewBag.Error = Resources.Resource.DatabaseError;
                Logger.Error(exception.Message);
                return View();
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);
                ViewBag.Error = Resources.Resource.UnexpectedError;
                return View();
            }
            return RedirectToAction("Index", new { id = idSpcr });
        }


        /// <summary>
        /// Изменение МИХ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMic(int id)
        {
            var mic = UnitOfWork.MicRepository.GetById(id);
            MassInertialCharactViewModel micVm;
            if (mic != null)
            {
                micVm = (MassInertialCharactViewModel)MassInerCharactMapper.Map(mic, typeof(MassInertialCharacteristic),
                typeof(MassInertialCharactViewModel));
            }
            else
            {
                ModelState.AddModelError("", Resources.Resource.NullMicError);
                micVm = new MassInertialCharactViewModel();
            }    
            return View(micVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMic(int id, MassInertialCharactViewModel micVm)
        {
            var idSpcr = Convert.ToInt32(Session["SpCrId"]);

            try
            {
                if (ModelState.IsValid && micVm.ID_MIC == id)
                {
                    var mic = (MassInertialCharacteristic)MassInerCharactMapper.Map(micVm, typeof(MassInertialCharactViewModel),
                    typeof(MassInertialCharacteristic));
                    UnitOfWork.MicRepository.Update(mic);
                    UnitOfWork.Save();
                    return RedirectToAction("Index", new { id = idSpcr });
                }
                return View(micVm);
            }

            catch (DbUpdateConcurrencyException exception)
            {
                ViewBag.Error = Resources.Resource.DatabaseError;
                Logger.Error(exception.Message);
                return View();
            }
            catch (Exception exception)
            {

                Logger.Error(exception.Message);
                ViewBag.Error = Resources.Resource.UnexpectedError;
                return RedirectToAction("EditMic", micVm);
            }
        }

        /// <summary>
        /// Удаление МИХ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteMic(int id)
        {
            var mic = UnitOfWork.MicRepository.GetById(id);
            var micVm = (MassInertialCharactViewModel)MassInerCharactMapper.Map(mic, typeof(MassInertialCharacteristic),
                typeof(MassInertialCharactViewModel));
            return View(micVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMic(int id, MassInertialCharactViewModel micVm)
        {
            int idSpcr = Convert.ToInt32(Session["SpCrId"]);
            try
            {
                if (id > 0)
                {
                    UnitOfWork.MicRepository.Delete(id);
                    UnitOfWork.Save();
                }
                return RedirectToAction("Index", new {id = idSpcr});
            }
            catch (DataException exception)
            {
                ViewBag.Error = Resources.Resource.DatabaseError;
                Logger.Error(exception.Message);
                return View();
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);
                ViewBag.Error = Resources.Resource.UnexpectedError;
                return View();
            }
        }
        /// <summary>
        /// Вычисляет элементы орбиты для заданного вектора состояния
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        public List<DiagramData> DiagramDatas { get; set; }


        /// <summary>
        /// Расчет прогноза
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Постороение гафиков
        /// </summary>
        /// <returns></returns>
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


    }
}