using System;
using System.Collections.Generic;
using System.Data;
<<<<<<< HEAD
using System.Linq;
using System.Net;
using System.Net.Http;
=======
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
>>>>>>> Admincontroller
using System.Web.Http;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Mappers;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using Ninject;
<<<<<<< HEAD
using NLog;
=======
>>>>>>> Admincontroller

namespace IntegratedFlghtDynamicSystem.Areas.Admin.Controllers
{
    public class SpacecraftController : ApiController
    {
        [Inject]
<<<<<<< HEAD
        public IUnitOfWork UnitOfWork { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
=======
        public IUnitOfWork UnitOfWork { get; set;}

        [Inject]
        public IMapper Mapper { get; set;}
>>>>>>> Admincontroller

        // GET api/Spacecraft
        public IEnumerable<SpacecraftViewModel> GetSpacecraftInitialDatas()
        {
            var spacecrafts = UnitOfWork.SpacecraftInfoRepository.Get();

            var spcrVm =
                spacecrafts.Select(
<<<<<<< HEAD
                    spcr => (SpacecraftViewModel)Mapper.Map(spcr, typeof(SpacecraftInitialData), typeof(SpacecraftViewModel))).ToList();
=======
                    spcr => (SpacecraftViewModel)Mapper.Map(spcr, typeof (SpacecraftInitialData), typeof (SpacecraftViewModel))).ToList();
>>>>>>> Admincontroller
            return spcrVm;
        }

        // GET api/Spacecraft/5
        public SpacecraftViewModel GetSpacecraftInitialData(int id)
        {
<<<<<<< HEAD
            var spcrVm = (SpacecraftViewModel)Mapper.Map(UnitOfWork.SpacecraftInfoRepository.GetById(id), typeof(SpacecraftInitialData),
                typeof(SpacecraftViewModel));
=======
            var spcrVm = (SpacecraftViewModel)Mapper.Map(UnitOfWork.SpacecraftInfoRepository.GetById(id), typeof (SpacecraftInitialData),
                typeof (SpacecraftViewModel));
>>>>>>> Admincontroller
            if (spcrVm == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return spcrVm;
        }

        //// PUT api/Spacecraft/5
        //public HttpResponseMessage PutSpacecraftInitialData(int id, SpacecraftInitialData spacecraftinitialdata)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    if (id != spacecraftinitialdata.SpacecraftInitDataId)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    db.Entry(spacecraftinitialdata).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        //// POST api/Spacecraft
<<<<<<< HEAD
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
                    //UnitOfWork.Save();
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
=======
        //public HttpResponseMessage PostSpacecraftInitialData(SpacecraftInitialData spacecraftinitialdata)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SpacecraftInitialDatas.Add(spacecraftinitialdata);
        //        db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, spacecraftinitialdata);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = spacecraftinitialdata.SpacecraftInitDataId }));
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}
>>>>>>> Admincontroller

        //// DELETE api/Spacecraft/5
        //public HttpResponseMessage DeleteSpacecraftInitialData(int id)
        //{
        //    SpacecraftInitialData spacecraftinitialdata = db.SpacecraftInitialDatas.Find(id);
        //    if (spacecraftinitialdata == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    db.SpacecraftInitialDatas.Remove(spacecraftinitialdata);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, spacecraftinitialdata);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}