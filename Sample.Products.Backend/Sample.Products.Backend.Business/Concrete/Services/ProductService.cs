using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Sample.Products.Backend.Api.Models;
using Sample.Products.Backend.Business.Abstract;
using Sample.Products.Backend.Business.Concrete.Attributes;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork.Paging;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Business.Concrete.Services
{
    [CatchErrorTypeForMethods]
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public ServiceResponse<Product> GetProductById(int id)
        {
            var rt = new ServiceResponse<Product>()
            {
                IsSuccessful = true,
                Entity = Repository.Single(x => x.Id == id,
                    include: x => x.Include(pt => pt.Brand)
                        .Include(pt => pt.ProductCategories)
                        .ThenInclude(pt=>pt.Category)
                        .Include(pt=>pt.ProductPictures)
                        .ThenInclude(pt => pt.Picture)
                        .Include(pt=>pt.ProductTags)
                        .ThenInclude(pt => pt.Tag)
                )
            };
            return rt;
        }

        public ServiceResponse<IPaginate<Product>> GetAllProducts(int page = 0, int pageIndex = 10)
        {
            var rt = new ServiceResponse<IPaginate<Product>>()
            {
                IsSuccessful = true,
                Entity = Repository.GetList(
                    include: x => x.Include(pt => pt.Brand)
                        .Include(pt => pt.ProductCategories)
                        .ThenInclude(pt=>pt.Category)
                        .Include(pt=>pt.ProductPictures)
                        .ThenInclude(pt => pt.Picture)
                        .Include(pt=>pt.ProductTags)
                        .ThenInclude(pt => pt.Tag),
                    index: page, size: pageIndex
                )
            };
            return rt;
        }

        public ServiceResponse<IPaginate<int>> GetTagIdsByProductId(int productId)
        {
            var savedProduct = Repository.Single(x => x.Id == productId,
                include: x => x.Include(pt=>pt.ProductTags)
                    .ThenInclude(pt => pt.Tag)
            ) ?? throw new ArgumentNullException(
                "Repository.Single(x => x.Id == productId,\n                include:x=>x.Include(st=>st.Tags)\n                )");
            
            var rt = new ServiceResponse<IPaginate<int>>()
            {
                IsSuccessful = true,
                Entity =savedProduct.ProductTags.Select(x=>x.Tag).Select(x=>x.Id).ToPaginate(0,int.MaxValue)
            };
            return rt;
        }

        public ServiceResponse<IPaginate<ProductModel>> GetProductsByTagIds(int[] ids,int pageIndex=0,int pageSize=20)
        {
            // if (ids == null) throw new ArgumentNullException(nameof(ids));
            Expression<Func<Product, bool>> exp = ids.IsNullOrEmpty()
                ? (Expression<Func<Product, bool>> )((x) => true)
                : ((x) => x.ProductTags.Any(st => ids.Contains(st.TagId)));
            var rt = new ServiceResponse<IPaginate<ProductModel>>()
            {
                IsSuccessful = true,
                Entity = Repository.GetList(
                    selector:x=>_mapper.Map<ProductModel>(x),
                    predicate:exp,
                    include:x=>x
                        .Include(pt=>pt.ProductTags)
                        .ThenInclude(pt=>pt.Tag)
                        .Include(pt=>pt.Brand)
                        // .ThenInclude(pt=>pt.Picture)
                        .Include(pt=>pt.ProductPictures)
                        // .ThenInclude(pt => pt.Picture)
                        .Include(pt=>pt.ProductCategories)
                        .ThenInclude(pt=>pt.Category),
                    
                    index:pageIndex,
                    size:pageSize,
                    disableTracking:false
                )
            };
            return rt;
        }

        public ServiceResponse<Product> DeleteProduct(int id)
        {
            var savedProduct = Repository.Single(x => x.Id == id) ?? throw new ArgumentNullException(
                "Repository.Single(x => x.Id == productId,\n                include:x=>x.Include(st=>st.Tags)\n                )");
            Repository.Delete(savedProduct);
            var rt = new ServiceResponse<Product>()
            {
                IsSuccessful = true,
                Entity =savedProduct
            };
            return rt;
        }

        public ServiceResponse<List<Product>> DeleteProducts(int[] ids)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));
            var savedProduct = Repository.GetList(x => ids.Contains(x.Id)) ?? throw new ArgumentNullException(
                "Repository.Single(x => x.Id == productId,\n                include:x=>x.Include(st=>st.Tags)\n                )");
            Repository.Delete(savedProduct);
            var rt = new ServiceResponse<List<Product>>()
            {
                IsSuccessful = true,
                Entity =savedProduct.Items.ToList()
            };
            return rt;
        }

        public ServiceResponse<Product> UpdateProduct(Product product)
        {
            var savedProduct = Repository.Single(x => x.Id==product.Id) ?? throw new ArgumentNullException(
                "Repository.Single(x => x.Id == productId,\n                include:x=>x.Include(st=>st.Tags)\n                )");
            Repository.Update(product);
            var rt = new ServiceResponse<Product>()
            {
                IsSuccessful = true,
                Entity =product
            };
            return rt;
        }

        public ServiceResponse<List<Product>> UpdateProducts(IEnumerable<Product> products)
        {
            if (products == null) throw new ArgumentNullException(nameof(products));
            
            foreach (var product in products)
            {
               var updatedResult= UpdateProduct(product);
               if (!updatedResult.IsSuccessful)
                   throw new Exception(updatedResult.ExceptionMessage);
            }

            return new ServiceResponse<List<Product>>()
            {
                IsSuccessful = true,
                Entity = products.ToList()
            };
        }
    }

    public interface IProductService
    {
        ServiceResponse<Product> GetProductById(int id);

        ServiceResponse<IPaginate<Product>> GetAllProducts(int page = 0, int pageIndex = 10);

        ServiceResponse<IPaginate<int>> GetTagIdsByProductId(int productId);

        ServiceResponse<IPaginate<ProductModel>> GetProductsByTagIds(int[] ids,int pageIndex=0,int pageSize=20);

        ServiceResponse<Product> DeleteProduct(int id);

        ServiceResponse<List<Product>> DeleteProducts(int[] ids);

        ServiceResponse<Product> UpdateProduct(Product product);

        ServiceResponse<List<Product>> UpdateProducts(IEnumerable<Product> products);
    }
}