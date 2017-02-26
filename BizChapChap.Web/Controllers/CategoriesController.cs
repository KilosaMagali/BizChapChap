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
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiControllerBase
    {
        private IEntityBaseRepository<Category> _categoryRepository;
        private IEntityBaseRepository<SubCategory> _subCategoryRepository;

        public CategoriesController(IEntityBaseRepository<Category> categoriesRepository, IEntityBaseRepository<SubCategory> subCategoriesRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) 
            : base(errorsRepository, unitOfWork)
        {
            _categoryRepository = categoriesRepository;
            _subCategoryRepository = subCategoriesRepository;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var categories = _categoryRepository.GetAll().ToList();

                IEnumerable<CategoryViewModel> categoriesVM = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);

                response = request.CreateResponse<IEnumerable<CategoryViewModel>>(HttpStatusCode.OK, categoriesVM);

                return response;
            });
        }

        [AllowAnonymous]
        [Route("subcategory/{id:int}")]
        public HttpResponseMessage GetSubCategories(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var subCategories = _subCategoryRepository.FindBy(p => p.CategoryRefId == id).ToList();

                IEnumerable<SubCategoryViewModel> subCategoriesVM = Mapper.Map<IEnumerable<SubCategory>, IEnumerable<SubCategoryViewModel>>(subCategories);

                response = request.CreateResponse<IEnumerable<SubCategoryViewModel>>(HttpStatusCode.OK, subCategoriesVM);

                return response;
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Post(HttpRequestMessage request, CategoryViewModel categoryVM)
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

                    var newCategory = Mapper.Map<CategoryViewModel, Category>(categoryVM);
                    _categoryRepository.Add(newCategory);

                    _unitOfWork.Commit();

                    // Update view model
                    categoryVM = Mapper.Map<Category, CategoryViewModel>(newCategory);
                    response = request.CreateResponse<CategoryViewModel>(HttpStatusCode.Created, categoryVM);
                }

                return response;
            });
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("addsubcategory")]
        public HttpResponseMessage PostSubCategory(HttpRequestMessage request, SubCategoryViewModel subCategoryVM)
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

                    var newSubCategory = Mapper.Map<SubCategoryViewModel, SubCategory>(subCategoryVM);
                    _subCategoryRepository.Add(newSubCategory);

                    _unitOfWork.Commit();

                    // Update view model
                    subCategoryVM = Mapper.Map<SubCategory, SubCategoryViewModel>(newSubCategory);
                    response = request.CreateResponse<SubCategoryViewModel>(HttpStatusCode.Created, subCategoryVM);
                }

                return response;
            });
        }
    }
}
