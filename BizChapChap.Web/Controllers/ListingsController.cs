using AutoMapper;
using BizChapChap.Web.Infrastructure;
using BizChapChap.Web.Models;
using BizChapChap.Web.Repository;
using BizChapChap.Web.ViewModels;
using System;
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

        [AllowAnonymous]
        [Route("{page:int=0}/{pageSize=3}/{subCategoryId?}/{regionId?}/{searchPhrase?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, int? subCategoryId = null, int? regionId = null, string searchPhrase = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Listing> listings = null;
                int totalListings = new int();

                if(subCategoryId != null && !string.IsNullOrEmpty(searchPhrase))
                {
                    listings = _listingRepository
                        .FindBy(m => m.Title.ToLower()
                        .Contains(searchPhrase.ToLower().Trim())
                            && m.SubCategoryRefId == subCategoryId)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalListings = _listingRepository
                        .FindBy(m => m.Title.ToLower()
                        .Contains(searchPhrase.ToLower().Trim())
                            && m.SubCategoryRefId == subCategoryId)
                        .Count();
                }
                else if(subCategoryId != null)
                {
                    listings = _listingRepository
                        .FindBy(m =>  m.SubCategoryRefId == subCategoryId)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalListings = _listingRepository
                        .FindBy(m => m.SubCategoryRefId == subCategoryId)
                        .Count();
                }
                else if(!string.IsNullOrEmpty(searchPhrase))
                {
                    listings = _listingRepository
                        .FindBy(m => m.Title.ToLower()
                        .Contains(searchPhrase.ToLower().Trim()))
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalListings = _listingRepository
                        .FindBy(m => m.Title.ToLower()
                        .Contains(searchPhrase.ToLower().Trim()))
                        .Count();
                }
                else //All null
                {
                    listings = _listingRepository
                        .GetAll()
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalListings = _listingRepository.GetAll().Count();
                }


                IEnumerable<ListingViewModel> listingsVM = Mapper.Map<IEnumerable<Listing>, IEnumerable<ListingViewModel>>(listings);

                PaginationSet<ListingViewModel> pagedSet = new PaginationSet<ListingViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalListings,
                    TotalPages = (int)Math.Ceiling((decimal)totalListings / currentPageSize),
                    Items = listingsVM
                };

                response = request.CreateResponse<PaginationSet<ListingViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }



        [HttpGet]
        [Route("featured")]
        public HttpResponseMessage GetFeatured(HttpRequestMessage request)
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


        [Route("delete")]
        public void Delete(int id)
        {
            
        }
    }
}
