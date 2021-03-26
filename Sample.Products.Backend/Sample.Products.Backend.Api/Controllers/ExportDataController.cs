using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.Business.Concrete.Models.ExportModels;
using Sample.Products.Backend.Business.Concrete.Services;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork;
using Sample.Products.Backend.Entities.Abstract;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportDataController:ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExportDataController(
            IFileService fileService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet("save-data")]
        public IActionResult ExportSave()
        {
            var rt = new ServiceResponse<bool>();
            try
            {
                ExportBrand();
                ExportCategory();
                ExportPicture();
                ExportProduct();
                ExportTag();
                ExportProductCategory();
                ExportProductPicture();
                ExportProductTag();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            rt.IsSuccessful = true;
            rt.Entity = true;
            return Ok(rt);
        }

        private void ExportBrand()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<Brand>();
            var brands=repo.GetList(index: 0, size: int.MaxValue);
            WriteJson<Brand>(brands.Items.ToList(),"BrandsData");
        }
        
        private void ExportCategory()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<Category>();
            var categories=repo.GetList(index: 0, size: int.MaxValue);
            WriteJson<Category>(categories.Items.ToList(),"CategoriesData");
        }
        
        private void ExportPicture()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<Picture>();
            const int size = 10;
            var page = 0;
            var pictures=repo.GetList(selector:x=>_mapper.Map<PictureExportModel>(x),index: page, size: size);
            while (pictures.HasNext)
            {
                if(page>12)
                    break;
                WriteJson<PictureExportModel>(pictures.Items.ToList(),$"PicturesData-{page+1}");
                page++;
                pictures=repo.GetList(selector:x=>_mapper.Map<PictureExportModel>(x),index: page, size: size);
            }
        }
        
        private void ExportProduct()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<Product>();
            var pictures=repo.GetList(index: 0, size: int.MaxValue);
            WriteJson<Product>(pictures.Items.ToList(),"ProductsData");
        }
        
        private void ExportProductCategory()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<ProductCategory>();
            var products=repo.GetList(index: 0, size: int.MaxValue);
            WriteJson<ProductCategory>(products.Items.ToList(),"ProductCategoriesData");
        }
        
        private void ExportProductPicture()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<ProductPicture>();
            var productPictures=repo.GetList(index: 0, size: int.MaxValue);
            WriteJson<ProductPicture>(productPictures.Items.ToList(),"ProductPicturesData");
        }
        
        private void ExportProductTag()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<ProductTag>();
            var productTags=repo.GetList(index: 0, size: int.MaxValue);
            WriteJson<ProductTag>(productTags.Items.ToList(),"ProductTagsData");
        }
        
        private void ExportTag()
        {
            var repo = _unitOfWork.GetReadOnlyRepository<Tag>();
            var tags=repo.GetList(index: 0, size: int.MaxValue);
            WriteJson<Tag>(tags.Items.ToList(),"TagsData");
        }

        private void WriteJson<T>(List<T> items,string fileNameWithoutExt)
        where T:class,new()
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(items, new JsonSerializerOptions()
            {
                IgnoreNullValues = false,
                PropertyNamingPolicy = null,
                PropertyNameCaseInsensitive = false,
            });
            var filePath = _fileService.GetAbsolutePath(_fileService.Combine("db", $"{fileNameWithoutExt}.json"));
            var dir = new FileInfo(filePath);
            if (!Directory.Exists(dir.DirectoryName))
            {
                Directory.CreateDirectory(dir.DirectoryName);
            }
            _fileService.WriteAllBytes(filePath,json);
        }
        
    }
}