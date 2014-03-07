using System;
using System.Collections.Generic;
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

namespace IntegratedFlghtDynamicSystem.Controllers
{
    //[Authorize(Roles = "admin")]
    public class NuController : ApiController
    {

        private readonly IMapper _nuMapper = new NuMapper();

        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        // GET api/Nu
        public IEnumerable<NuViewModel> GetNUs()
        {
            var nus = UnitOfWork.NuRepository.Get();
            var nuViewModels = nus.Select(nu => (NuViewModel)_nuMapper.Map(nu, typeof(NU), typeof(NuViewModel))).ToList();
            return nuViewModels;
        }

        //// GET api/Nu/5
        public NuViewModel GetNu(int id)
        {
            var nu = UnitOfWork.NuRepository.GetById(id);
            if (nu == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            var nuViewModel = (NuViewModel)_nuMapper.Map(nu, typeof (NU), typeof (NuViewModel));
            return nuViewModel;
        }

        //// PUT api/Nu/5
        public HttpResponseMessage PutNu(int id, NuViewModel nuVm)
        {
            if (!ModelState.IsValid || nuVm == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");
            }

            if (id != nuVm.ID_NU)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var nu = (NU)_nuMapper.Map(nuVm, typeof (NuViewModel), typeof (NU));
            UnitOfWork.NuRepository.Update(nu);

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

        //// POST api/Nu
        public HttpResponseMessage PostNu(NuViewModel nuVm)
        {
            if (ModelState.IsValid && nuVm != null)
            {
                var nu = (NU)_nuMapper.Map(nuVm, typeof(NuViewModel), typeof(NU));
                UnitOfWork.NuRepository.Insert(nu);
                UnitOfWork.Save();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, nu);
                var link = Url.Link("DefaultApi", new {id = nu.ID_NU});
                if (link != null)
                {
                    response.Headers.Location = new Uri(link);
                }
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request");
        }

        //// DELETE api/Nu/5
        public HttpResponseMessage DeleteNu(int id)
        {
            var nu = UnitOfWork.NuRepository.GetById(id);
            if (nu == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            UnitOfWork.NuRepository.Delete(nu);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, nu);
        }
    }
}