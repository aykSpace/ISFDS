using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Controllers;
using IntegratedFlghtDynamicSystem.Models;

namespace IntegratedFlghtDynamicSystem.Areas.Default.Controllers
{
    public class MassInerCharacteristicController : BaseController
    {
        /// <summary>
        /// Добавить новые МИХ
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public PartialViewResult AddMic()
        {
            return PartialView();
        }

        [System.Web.Mvc.HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddMic(MassInertialCharactViewModel micViewModel)
        {
            var dateMic = Request["DateOfID"] ?? micViewModel.DateOfID.ToString("d", CultureInfo.InvariantCulture);
            const string format = "MM/dd/yyyy";
            var provider = CultureInfo.InvariantCulture;
            micViewModel.DateOfID = DateTime.ParseExact(dateMic, format, provider);
            try
            {
                var mic =
                    (MassInertialCharacteristic)
                        MassInerCharactMapper.Map(micViewModel, typeof (MassInertialCharactViewModel),
                            typeof (MassInertialCharacteristic));
                UnitOfWork.MicRepository.Insert(mic);
                if (Session["SpCrId"] != null)
                {
                    int idSpcr = Convert.ToInt32(Session["SpCrId"]);
                    UnitOfWork.SpacecraftCommonDataRepository.Insert(new SpaceсraftCommonData
                    {
                        SpacecraftInitDataId = idSpcr
                    });
                }
                ViewBag.Success = "Mic was added!";
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
                return Json(new { Message = ViewBag.Success});   
            }
            return View();
        }

    }
}
