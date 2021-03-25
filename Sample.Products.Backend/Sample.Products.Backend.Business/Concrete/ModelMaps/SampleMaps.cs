using System.Linq;
using AutoMapper;
using Sample.Products.Backend.Api.Models;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Business.Concrete.ModelMaps
{
    public class SampleMaps:Profile
    {
        public SampleMaps()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<Product, ProductModel>()
                .ForMember(x=>x.Categories,opt=>opt.MapFrom(x=>x.ProductCategories.Select(pt=>pt.Category)))
                .ForMember(x=>x.CategoryIds,opt=>opt.MapFrom(x=>x.ProductCategories.Select(pt=>pt.CategoryId)))
                .ForMember(x=>x.PictureIds,opt=>opt.MapFrom(x=>x.ProductPictures.Select(pt=>pt.PictureId)))
                .ForMember(x=>x.Tags,opt=>opt.MapFrom(x=>x.ProductTags.Select(pt=>pt.Tag)));
            CreateMap<Tag, TagModel>();
            CreateMap<Brand, BrandModel>();
            CreateMap<Picture, PictureModel>();
        }
    }
}