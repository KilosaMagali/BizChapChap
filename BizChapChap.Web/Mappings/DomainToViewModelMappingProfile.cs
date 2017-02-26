using AutoMapper;
using BizChapChap.Web.Models;
using BizChapChap.Web.ViewModels;

namespace BizChapChap.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }

        protected override void Configure()
        {
            CreateMap<Listing, ListingViewModel>();

            CreateMap<Category, CategoryViewModel>();

            CreateMap<SubCategory, SubCategoryViewModel>();
        }
    }
}