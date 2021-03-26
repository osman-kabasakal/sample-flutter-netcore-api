using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Business.Abstract
{
    public abstract class BaseService<T>
    where T:class,IEntity,new()
    {
        private IRepository<T> _repository;
        protected IRepository<T> Repository => _repository ??= _unitOfWork.GetRepository<T>();
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}