using AutoMapper;
using BizChapChap.Web.Infrastructure;
using BizChapChap.Web.Models;
using BizChapChap.Web.Repository;
using BizChapChap.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizChapChap.Web.Controllers
{
    [RoutePrefix("api/listings")]
    public class ListingsController : ApiControllerBase
    {
        private IEntityBaseRepository<Listing> _listingRepository;



        public ListingsController(IEntityBaseRepository<Listing> listingRepository,
                              IEntityBaseRepository<Error> _errorsRepository,
                              IUnitOfWork _unitOfWork): base(_errorsRepository, _unitOfWork)
        {
            _listingRepository = listingRepository;
        }


        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var listings = _listingRepository.GetAll().OrderByDescending(m => m.Date).Take(6).ToList();

                IEnumerable<ListingViewModel> listingsVM = Mapper.Map<IEnumerable<Listing>, IEnumerable<ListingViewModel>>(listings);

                response = request.CreateResponse<IEnumerable<ListingViewModel>>(HttpStatusCode.OK, listingsVM);

                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ListingViewModel listingViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    listingViewModel.DateEdit = listingViewModel.Date;
                    var newListing = Mapper.Map<ListingViewModel, Listing>(listingViewModel);
                    _listingRepository.Add(newListing);

                    _unitOfWork.Commit();

                    // Update view model
                    listingViewModel = Mapper.Map<Listing, ListingViewModel>(newListing);
                    response = request.CreateResponse<ListingViewModel>(HttpStatusCode.Created, listingViewModel);
                }

                return response;
            });
        }
    }
}
