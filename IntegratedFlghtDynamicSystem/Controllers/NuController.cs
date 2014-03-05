using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IntegratedFlghtDynamicSystem.Areas.Default.ViewModels;
using IntegratedFlghtDynamicSystem.Mappers;
using IntegratedFlghtDynamicSystem.Models;
using IntegratedFlghtDynamicSystem.Models.DataTools;

namespace IntegratedFlghtDynamicSystem.Controllers
{
    //[Authorize(Roles = "admin")]
    public class NuController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IMapper _nuMapper = new NuMapper();

        // GET api/Nu
        public IEnumerable<NuViewModel> GetNUs()
        {
            var nus = _unitOfWork.NuRepository.Get();
            var nuViewModels = nus.Select(nu => (NuViewModel)_nuMapper.Map(nu, typeof(NU), typeof(NuViewModel))).ToList();
            return nuViewModels;
        }

        //// GET api/Nu/5
        public NuViewModel GetNu(int id)
        {
            var nu = _unitOfWork.NuRepository.GetById(id);
            if (nu == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            var nuViewModel = (NuViewModel)_nuMapper.Map(nu, typeof (NU), typeof (NuViewModel));
            return nuViewModel;
        }

        //// PUT api/Nu/5
        //public HttpResponseMessage PutNU(int id, NU nu)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    if (id != nu.ID_NU)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    db.Entry(nu).State = EntityState.Modified;

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

        //// POST api/Nu
        //public HttpResponseMessage PostNU(NU nu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.NUs.Add(nu);
        //        db.SaveChanges();

        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, nu);
        //        response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = nu.ID_NU }));
        //        return response;
        //    }
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //    }
        //}

        //// DELETE api/Nu/5
        //public HttpResponseMessage DeleteNU(int id)
        //{
        //    NU nu = db.NUs.Find(id);
        //    if (nu == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    db.NUs.Remove(nu);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, nu);
        //}
    }
}