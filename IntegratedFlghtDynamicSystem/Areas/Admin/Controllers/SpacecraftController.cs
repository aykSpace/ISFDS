using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Mappers;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using Ninject;
using NLog;

namespace IntegratedFlghtDynamicSystem.Areas.Admin.Controllers
{
    public class SpacecraftController : ApiController
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        /*public  IMapper Mapper = new SpacecraftMapper();*/

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        // GET api/Spacecraft
        public IEnumerable<SpacecraftViewModel> GetSpacecraftInitialDatas()
        {
            var spacecrafts = UnitOfWork.SpacecraftInfoRepository.Get();

            var spcrVm =
                spacecrafts.Select(
                    spcr => (SpacecraftViewModel)Mapper.Map(spcr, typeof(SpacecraftInitialData), typeof(SpacecraftViewModel))).ToList();
            return spcrVm;
        }

        // GET api/Spacecraft/5
        public SpacecraftViewModel GetSpacecraftInitialData(int id)
        {
            var spcrVm = (SpacecraftViewModel)Mapper.Map(UnitOfWork.SpacecraftInfoRepository.GetById(id), typeof(SpacecraftInitialData),
                typeof(SpacecraftViewModel));
            if (spcrVm == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return spcrVm;
        }

        //// PUT api/Spacecraft/5
        public HttpResponseMessage PutSpacecraftInitialData(int id, SpacecraftViewModel spacecraftViewModel)
        {
            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors).AsEnumerable();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != spacecraftViewModel.SpacecraftInitDataId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var spcraft = (SpacecraftInitialData)Mapper.Map(spacecraftViewModel, typeof(SpacecraftViewModel),
                    typeof(SpacecraftInitialData));
            UnitOfWork.SpacecraftInfoRepository.Update(spcraft);

            try
            {
               UnitOfWork.Save();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //// POST api/Spacecraft
        [HttpPost]
        public HttpResponseMessage PostSpacecraftInitialData(SpacecraftViewModel spacecraftViewModel)
        {
            if (ModelState.IsValid)
            {
                var spcraft = (SpacecraftInitialData)Mapper.Map(spacecraftViewModel, typeof(SpacecraftViewModel),
                    typeof(SpacecraftInitialData));
                UnitOfWork.SpacecraftInfoRepository.Insert(spcraft);
                try
                {
                    UnitOfWork.Save();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, spcraft);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = spcraft.SpacecraftInitDataId }));
                    return response;
                }
                catch (DataException exception)
                {
                    _logger.Error(exception.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data error!");

                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");
        }

        //// DELETE api/Spacecraft/5
        public HttpResponseMessage DeleteSpacecraftInitialData(int id)
        {
            SpacecraftInitialData spacecraftinitialdata = UnitOfWork.SpacecraftInfoRepository.GetById(id);
            if (spacecraftinitialdata == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            UnitOfWork.SpacecraftInfoRepository.Delete(spacecraftinitialdata);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, spacecraftinitialdata);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}