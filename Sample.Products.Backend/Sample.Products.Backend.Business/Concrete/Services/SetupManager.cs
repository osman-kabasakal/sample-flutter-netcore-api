using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sample.Products.Backend.Business.Concrete.Models.ExportModels;
using Sample.Products.Backend.Core.EqualiyComparers;
using Sample.Products.Backend.DataAccess.Abstract;
using Sample.Products.Backend.DataAccess.Concrete.EntityFramework.Context;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork;
using Sample.Products.Backend.Entities.Abstract;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Business.Concrete.Services
{
    public class SetupManager : ISetupManager, ISeeder
    {
        private readonly IFileService _fileService;

        public SetupManager(
            IFileService fileService
        )
        {
            _fileService = fileService;
        }

        public void setup()
        {
            ImportPictures();
            ImportBrands();
            ImportCategories();
            ImportTags();
            ImportProducts();
            ImportProductCategories();
            ImportProductPictures();
            ImportProductTags();
        }

        public List<Picture> ImportPictures()
        {
            var rt = new List<Picture>();
            try
            {
                var fileNamePattern = $"PicturesData-*.json";
                var fileInfo = new FileInfo(_fileService.GetAbsolutePath(_fileService.Combine("db", fileNamePattern)));
                if (Directory.Exists(fileInfo.DirectoryName))
                {
                    var files = Directory.GetFiles(fileInfo.DirectoryName, fileNamePattern);
                    foreach (var file in files)
                    {
                        using (var fileContent = File.OpenRead(file))
                        {
                            var pictures = JsonSerializer
                                .DeserializeAsync<List<Picture>>(fileContent, new JsonSerializerOptions()
                                {
                                    IgnoreNullValues = false,
                                    PropertyNamingPolicy = null,
                                    PropertyNameCaseInsensitive = false,
                                }).ConfigureAwait(true).GetAwaiter().GetResult();
                            rt.AddRange(pictures);
                            // _unitContext.Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [SampleProductsFillFolder1].[dbo].[Pictures] off");
                            // // _unitContext.Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [SampleProductsFillFolder1].[dbo].[Pictures] on");
                            //
                            // repo.Add(pictures);
                            // _unitOfWork.SaveChanges();
                            // _unitContext.Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [SampleProductsFillFolder1].[dbo].[Pictures] OFF");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return rt;
            }

            return rt.Distinct(new LambdaDistinct<Picture>(((xl, xr) => xl.Id == xr.Id), x => x.Id.GetHashCode()))
                .ToList();
        }

        public List<Brand> ImportBrands()
        {
            var fileNamePattern = $"BrandsData.json";
            return ImportDataFromJson<Brand>(fileNamePattern, "Brands");
        }

        public List<Category> ImportCategories()
        {
            var fileNamePattern = $"CategoriesData.json";
            var pictures = ImportPictures();
            return ImportDataFromJson<Category>(fileNamePattern, "Categories")
                .Where(x=>pictures.Any(pt=>pt.Id==x.PictureId))
                .Distinct(new LambdaDistinct<Category>(
                    (xl, xr) => xl.PictureId == xr.PictureId,
                    x => x.PictureId.GetHashCode())).ToList();
        }

        public List<Product> ImportProducts()
        {
            var fileNamePattern = $"ProductsData.json";
            return ImportDataFromJson<Product>(fileNamePattern, "Products")
                .Distinct(new LambdaDistinct<Product>(
                    ((xl, xr) => xl.BrandId == xr.BrandId),
                    x => x.BrandId))
                .ToList();
        }

        public List<Tag> ImportTags()
        {
            var fileNamePattern = $"TagsData.json";
            return ImportDataFromJson<Tag>(fileNamePattern, "Tags");
        }

        public List<ProductCategory> ImportProductCategories()
        {
            var fileNamePattern = $"ProductCategoriesData.json";
            var category = ImportCategories();
            var product = ImportProducts();
            return ImportDataFromJson<ProductCategory>(fileNamePattern, "ProductCategory")
                .Where(x=>category.Any(pt=>pt.Id==x.CategoryId))
                .Where(x=>product.Any(pt=>pt.Id==x.ProductId))
                .Where(x =>
                    (new[] {332, 724, 303}).All(n => x.ProductId != n)
                    && (new[] {36, 10, 46}).All(n => n != x.CategoryId))
                .Distinct(new LambdaDistinct<ProductCategory>(
                    (xl, xr) =>
                        xl.CategoryId == xr.CategoryId && xl.ProductId == xr.ProductId,
                    x => x.ProductId.GetHashCode())
                )
                .ToList();
        }

        public List<ProductTag> ImportProductTags()
        {
            var fileNamePattern = $"ProductTagsData.json";
            var products = ImportProducts();
            var tags = ImportTags();
            return ImportDataFromJson<ProductTag>(fileNamePattern, "ProductTags")
                .Where(x=>products.Any(pt=>pt.Id==x.ProductId))
                .Where(x=>tags.Any(pt=>pt.Id==x.TagId))
                .Distinct(new LambdaDistinct<ProductTag>(
                    (xl, xr) =>
                        xl.ProductId == xr.ProductId && xl.TagId == xr.TagId,
                    x => x.ProductId.GetHashCode())
                )
                .ToList();
        }

        public List<ProductPicture> ImportProductPictures()
        {
            var fileNamePattern = $"ProductPicturesData.json";
            var pictures = ImportPictures();
            var products = ImportProducts();
            return ImportDataFromJson<ProductPicture>(fileNamePattern, "ProductPictures")
                .Where(x=>pictures.Any(pt=>pt.Id==x.PictureId))
                .Where(x=>products.Any(pt=>pt.Id==x.ProductId))
                .Distinct(new LambdaDistinct<ProductPicture>(
                    (xl, xr) =>
                        xl.PictureId == xr.PictureId && xl.ProductId == xr.ProductId,
                    x => x.ProductId.GetHashCode())
                ).ToList();
        }

        private List<TRepo> ImportDataFromJson<TRepo>(string filename, string tableName)
            where TRepo : class, IEntity, new()
        {
            var fileInfo = new FileInfo(_fileService.GetAbsolutePath(_fileService.Combine("db", filename)));
            var rt = new List<TRepo>();
            if (fileInfo.Exists)
            {
                // var files = Directory.GetFiles(fileInfo.DirectoryName, fileNamePattern);
                // var repo = _unitOfWork.GetRepository<TRepo>();
                using (var fileContent = fileInfo.OpenRead())
                {
                    var categories = JsonSerializer
                        .DeserializeAsync<List<TRepo>>(fileContent, new JsonSerializerOptions()
                        {
                            IgnoreNullValues = false,
                            PropertyNamingPolicy = null,
                            PropertyNameCaseInsensitive = false,
                        }).ConfigureAwait(true).GetAwaiter().GetResult();
                    rt = categories;
                    // _unitContext.Context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [SampleProductsFillFolder1].[dbo].[{tableName}] ON");
                    // repo.Add(categories);
                    // _unitOfWork.SaveChanges();
                    // _unitContext.Context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT [SampleProductsFillFolder1].[dbo].[{tableName}] OFF");
                }
            }

            return rt;
        }

        public List<T> GetSeedData<T>() where T : class, IEntity, new()
        {
            if (typeof(T).ToString() == typeof(Product).ToString())
            {
                return ImportProducts() as List<T>;
            }

            if (typeof(T).ToString() == typeof(Picture).ToString())
            {
                return ImportPictures() as List<T>;
            }

            if (typeof(T).ToString() == typeof(Brand).ToString())
            {
                return ImportBrands() as List<T>;
            }

            if (typeof(T).ToString() == typeof(Category).ToString())
            {
                return ImportCategories() as List<T>;
            }

            if (typeof(T).ToString() == typeof(Tag).ToString())
            {
                return ImportTags() as List<T>;
            }

            if (typeof(T).ToString() == typeof(ProductPicture).ToString())
            {
                return ImportProductPictures() as List<T>;
            }

            if (typeof(T).ToString() == typeof(ProductTag).ToString())
            {
                return ImportProductTags() as List<T>;
            }

            if (typeof(T).ToString() == typeof(ProductCategory).ToString())
            {
                return ImportProductCategories() as List<T>;
            }

            return null;
        }
    }

    public interface ISetupManager
    {
        void setup();
        List<Picture> ImportPictures();
        List<Brand> ImportBrands();
        List<Category> ImportCategories();
        List<Tag> ImportTags();
        List<Product> ImportProducts();
        List<ProductCategory> ImportProductCategories();
        List<ProductPicture> ImportProductPictures();
        List<ProductTag> ImportProductTags();
    }
}