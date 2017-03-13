using BizChapChap.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BizChapChap.Web.Models;
using BizChapChap.Web.Repository;

namespace BizChapChap.Web.Controllers
{
    [RoutePrefix("api/regions")]
    public class RegionsController : ApiControllerBase
    {
        private IEntityBaseRepository<Region> _regionRepository;
        public RegionsController(IEntityBaseRepository<Region> regionsRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
        {
            _regionRepository = regionsRepository;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var regions = _regionRepository.GetAll().ToList();

               
                response = request.CreateResponse<IEnumerable<Region>>(HttpStatusCode.OK, regions);

                return response;
            });
        }
    }
}
