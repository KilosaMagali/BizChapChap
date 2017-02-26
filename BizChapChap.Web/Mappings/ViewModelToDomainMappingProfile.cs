using AutoMapper;
using BizChapChap.Web.Models;
using BizChapChap.Web.ViewModels;

namespace BizChapChap.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<ListingViewModel,Listing>();

            CreateMap<CategoryViewModel, Category>();

            CreateMap<SubCategoryViewModel, SubCategory>();
        }
    }
}