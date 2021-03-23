using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork.Paging;

namespace Sample.Products.Backend.DataAccess.Concrete.UnitOfWork
{
    public interface IRepositoryReadOnly<T> : IReadRepository<T> where T : class
    {
       
    }
}