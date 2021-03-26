using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.Products.Backend.Business.Concrete.Attributes;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork.Paging;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Business.Concrete.Services
{
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private IRepository<Tag> _repository;

        public TagService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IUnitOfWork _unitOfWork { get; }
        public IRepository<Tag> Repository => _repository ??= _unitOfWork.GetRepository<Tag>();


        [CatchError]
        public ServiceResponse<Tag> GetTagById(int id)
        {
            var rt = new ServiceResponse<Tag>();
            var tag = Repository.Single(x => x.Id == id
                      ) ??
                      throw new ArgumentNullException("Repository.Single(x => x.Id == id)");
            rt.IsSuccessful = true;
            rt.Entity = tag;
            rt.Count = 0;
            return rt;
        }

        [CatchError]
        public ServiceResponse<IPaginate<Tag>> GetAllTags(int page = 0, int pageSize = 10)
        {
            var rt = new ServiceResponse<IPaginate<Tag>>();
            rt.IsSuccessful = true;
            var tags = Repository.GetList(index: page, size: pageSize);
            rt.Entity = tags;
            rt.Count = rt.Entity.Count;
            return rt;
        }

        [CatchError]
        public ServiceResponse<IPaginate<Tag>> GetProductTags(int productId, int page = 0, int pageIndex = int.MaxValue)
        {
            var rt = new ServiceResponse<IPaginate<Tag>>
            {
                IsSuccessful = true,
                Entity = Repository.GetList(x => x.ProductTags.Any(pt => pt.ProductId == productId), index: page, size: pageIndex,
                    include: x => x.Include(pt => pt.ProductTags).ThenInclude(pt => pt.Product))
            };
            rt.Count = rt.Entity.Count;
            return rt;
        }

        [CatchError]
        public ServiceResponse<IPaginate<int>> GetProductsIdsByTagId(int tagId)
        {
            var tag = Repository.Single(x => x.Id == tagId, include: x => x.Include(pt => pt.ProductTags).ThenInclude(pt => pt.Product)) ??
                      throw new ArgumentNullException(
                          "Repository.Single(x => x.Id == tagId, include: x => x.Include(pt => pt.Products))");

            var rt = new ServiceResponse<IPaginate<int>>
            {
                IsSuccessful = true,
                Entity = tag.ProductTags.Select(x=>x.Tag).ToPaginate(converter: x => x.Select(p => p.Id), 0, int.MaxValue)
            };
            return rt;
        }

        [CatchError]
        public ServiceResponse<IPaginate<Tag>> GetTagsByIds(int[] ids)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));
            var rt = new ServiceResponse<IPaginate<Tag>>()
            {
                IsSuccessful = true,
                Entity = Repository.GetList(predicate: x => ids.Contains(x.Id), index: 0, size: int.MaxValue)
            };
            rt.Count = rt.Entity.Count;
            return rt;
        }

        [CatchError]
        public ServiceResponse<Tag> DeleteTag(int id)
        {
            var savedTag = Repository.Single(x => x.Id == id) ??
                           throw new ArgumentNullException("Repository.Single(x => x.Id == id)");
            Repository.Delete(savedTag);
            var rt = new ServiceResponse<Tag>()
            {
                IsSuccessful = true,
                Entity = savedTag
            };
            return rt;
        }

        [CatchError]
        public ServiceResponse<List<Tag>> DeleteTags(int[] ids)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));
            
            var tags = Repository.GetList(x => ids.Contains(x.Id), index: 0, size: int.MaxValue);
            Repository.Delete(tags.Items);
            var rt = new ServiceResponse<List<Tag>>()
            {
                IsSuccessful = true,
                Entity = tags.Items.ToList(),
                Count = tags.Count
            };
            return rt;
        }

        [CatchError]
        public ServiceResponse<Tag> UpdateTag(Tag tag)
        {
            var savedTag = Repository.Single(x => x.Id == tag.Id) ??
                           throw new ArgumentNullException("Repository.Single(x => x.Id == tag.Id)");
            Repository.Update(tag);
            var rt = new ServiceResponse<Tag>()
            {
                IsSuccessful = true,
                Entity = tag
            };
            return rt;
        }

        [CatchError]
        public ServiceResponse<List<Tag>> UpdateTags(IEnumerable<Tag> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));
            foreach (var tag in tags)
            {
                var updatedTag=UpdateTag(tag);
                if (!updatedTag.IsSuccessful)
                    throw new Exception(updatedTag.ExceptionMessage);
            }
            var rt = new ServiceResponse<List<Tag>>()
            {
                IsSuccessful = true,
                Entity = tags.ToList(),
                Count = tags.Count()
            };
            return rt;
        }
    }

    public interface ITagService
    {
        [CatchError]
        ServiceResponse<Tag> GetTagById(int id);

        [CatchError]
        ServiceResponse<IPaginate<Tag>> GetAllTags(int page = 0, int pageIndex = 10);

        ServiceResponse<IPaginate<Tag>> GetProductTags(int productId, int page = 0, int pageIndex = int.MaxValue);

        ServiceResponse<IPaginate<int>> GetProductsIdsByTagId(int tagId);

        ServiceResponse<IPaginate<Tag>> GetTagsByIds(int[] ids);

        ServiceResponse<Tag> DeleteTag(int id);

        ServiceResponse<List<Tag>> DeleteTags(int[] ids);

        ServiceResponse<Tag> UpdateTag(Tag tag);

        ServiceResponse<List<Tag>> UpdateTags(IEnumerable<Tag> tags);
    }
}