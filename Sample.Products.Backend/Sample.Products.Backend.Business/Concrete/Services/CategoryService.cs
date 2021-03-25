using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public ServiceResponse<CategoryModel> GetCategoryById(int id)
        {
            var rt = new ServiceResponse<CategoryModel>()
            {
                IsSuccessful = true,
                Entity = Repository.Single<CategoryModel>(x => x.Id == id,
                             include: x => x.Include(pt => pt.ParentCategory).Include(pt => pt.Picture)) ??
                         throw new NullReferenceException("Category Bulunamadı.")
            };
            return rt;
        }

        public ServiceResponse<List<CategoryModel>> GetCategoryTree()
        {
            var allCategories = Repository.GetList(size: int.MaxValue, index: 0,
                selector: x => _mapper.Map<CategoryModel>(x)
                , include: x =>
                    x.Include(pt => pt.ParentCategory),
                        // .Include(pt => pt.Picture),
                disableTracking: false);
            var onlyParentCategory = allCategories.Items.ToList().Where(x => x.ParentId == null);
            var rt = new ServiceResponse<List<CategoryModel>>()
            {
                IsSuccessful = true,
                Entity = onlyParentCategory.ToPaginate(0, int.MaxValue).Items.ToList(),
            };
            rt.Count = rt.Entity.Count;
            return rt;
        }

        public ServiceResponse<IPaginate<CategoryModel>> GetAllCategories(int page = 0, int pageSize = 10)
        {
            var rt = new ServiceResponse<IPaginate<CategoryModel>>()
            {
                IsSuccessful = true,
                Entity = Repository.GetList(size: pageSize, index: page,
                    selector: x => _mapper.Map<CategoryModel>(x)
                    , include: x =>
                        x.Include(pt => pt.ParentCategory)
                            .Include(pt => pt.Picture),
                    disableTracking: false),
            };
            rt.Count = rt.Entity.Count;
            return rt;
        }

        public ServiceResponse<IPaginate<CategoryModel>> GetProductCategories(int productId, int page = 0,
            int pageSize = int.MaxValue)
        {
            var allCategories = Repository.GetList(size: int.MaxValue, index: 0,
                selector: x => _mapper.Map<CategoryModel>(x)
                , include: x =>
                    x.Include(pt => pt.ParentCategory)
                        .Include(pt => pt.Picture),
                disableTracking: false);
            var productCat=Repository.GetList(
                predicate: x => x.ProductCategories.Select(st => st.ProductId).Contains(productId),
                index: page,
                size: pageSize
            );
            var rt = new ServiceResponse<IPaginate<CategoryModel>>()
            {
                IsSuccessful = true,
                Entity = allCategories.Items.Where(x=>productCat.Items.Any(pt=>pt.Id==x.Id)).ToPaginate(page,pageSize)
            };
            rt.Count = rt.Entity.Count;
            return rt;
        }

        public ServiceResponse<IPaginate<int>> GetProductsIdsByCategoryId(int categoryId)
        {
            var savedCategory =
                Repository.Single(x => x.Id == categoryId, include: x => x.Include(pt=>pt.ProductCategories).ThenInclude(pt => pt.Product)) ??
                throw new ArgumentNullException("Repository.Single(x => x.Id == categoryId)");

            var rt = new ServiceResponse<IPaginate<int>>()
            {
                IsSuccessful = true,
                Entity = savedCategory.ProductCategories.Select(x => x.ProductId).ToPaginate(0, int.MaxValue)
            };
            return rt;
        }

        public ServiceResponse<IPaginate<CategoryModel>> GetCategoriesByIds(int[] ids)
        {
            var rt = new ServiceResponse<IPaginate<CategoryModel>>()
            {
                IsSuccessful = true,
                Entity = Repository.GetList(
                    selector: x => _mapper.Map<CategoryModel>(x),
                    predicate: x => ids.Contains(x.Id),
                    include: x =>
                        x.Include(pt => pt.Picture)
                            .Include(pt => pt.ParentCategory)
                )
            };

            return rt;
        }

        public ServiceResponse<CategoryModel> DeleteCategory(int id)
        {
            var savedCategory = Repository.Single(x => x.Id == id) ??
                                throw new ArgumentNullException("Repository.Single(x => x.Id == id)");
            Repository.Delete(savedCategory);
            var rt = new ServiceResponse<CategoryModel>()
            {
                IsSuccessful = true,
                Entity = _mapper.Map<CategoryModel>(savedCategory)
            };
            return rt;
        }

        public ServiceResponse<List<CategoryModel>> DeleteCategories(int[] ids)
        {
            var categories = Repository.GetList(x => ids.Contains(x.Id), index: 0, size: int.MaxValue);
            Repository.Delete(categories);
            var rt = new ServiceResponse<List<CategoryModel>>()
            {
                IsSuccessful = true,
                Entity = _mapper.Map<List<CategoryModel>>(categories.Items.ToList())
            };
            return rt;
        }

        public ServiceResponse<CategoryModel> UpdateCategory(Category category)
        {
            var savedCategory = Repository.Single(x => x.Id == category.Id) ??
                                throw new ArgumentNullException("Repository.Single(x => x.Id == category.Id)");
            Repository.Update(category);
            var rt = new ServiceResponse<CategoryModel>()
            {
                IsSuccessful = true,
                Entity = _mapper.Map<CategoryModel>(category)
            };
            return rt;
        }

        public ServiceResponse<List<CategoryModel>> UpdateCategories(IEnumerable<Category> categories)
        {
            if (categories == null) throw new ArgumentNullException(nameof(categories));
            foreach (var category in categories)
            {
                var updatedCategory = UpdateCategory(category);
                if (!updatedCategory.IsSuccessful)
                    throw new Exception(updatedCategory.ExceptionMessage);
            }

            var rt = new ServiceResponse<List<CategoryModel>>()
            {
                IsSuccessful = true,
                Entity = _mapper.Map<List<CategoryModel>>(categories.ToList())
            };
            return rt;
        }
    }

    public interface ICategoryService
    {
        [CatchError]
        ServiceResponse<CategoryModel> GetCategoryById(int id);

        ServiceResponse<List<CategoryModel>> GetCategoryTree();

        [CatchError]
        ServiceResponse<IPaginate<CategoryModel>> GetAllCategories(int page = 0, int pageSize = 10);

        ServiceResponse<IPaginate<CategoryModel>> GetProductCategories(int productId, int page = 0,
            int pageSize = int.MaxValue);

        ServiceResponse<IPaginate<int>> GetProductsIdsByCategoryId(int tagId);

        ServiceResponse<IPaginate<CategoryModel>> GetCategoriesByIds(int[] ids);

        ServiceResponse<CategoryModel> DeleteCategory(int id);

        ServiceResponse<List<CategoryModel>> DeleteCategories(int[] ids);

        ServiceResponse<CategoryModel> UpdateCategory(Category tag);

        ServiceResponse<List<CategoryModel>> UpdateCategories(IEnumerable<Category> tags);
    }
}