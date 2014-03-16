﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Mappers;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;
using Ninject;

namespace IntegratedFlghtDynamicSystem.Areas.Admin.Controllers
{
    public class SpacecraftController : ApiController
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set;}

        [Inject]
        public IMapper Mapper { get; set;}

        // GET api/Spacecraft
        public IEnumerable<SpacecraftViewModel> GetSpacecraftInitialDatas()
        {
            var spacecrafts = UnitOfWork.SpacecraftInfoRepository.Get();

            var spcrVm =
                spacecrafts.Select(
                    spcr => (SpacecraftViewModel)Mapper.Map(spcr, typeof (SpacecraftInitialData), typeof (SpacecraftViewModel))).ToList();
            return spcrVm;
        }

        // GET api/Spacecraft/5
        public SpacecraftViewModel GetSpacecraftInitialData(int id)
        {
            var spcrVm = (SpacecraftViewModel)Mapper.Map(UnitOfWork.SpacecraftInfoRepository.GetById(id), typeof (SpacecraftInitialData),
                typeof (SpacecraftViewModel));
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