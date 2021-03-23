using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Sample.Products.Backend.DataAccess.Concrete.UnitOfWork
{
    public class RepositoryReadOnly<T> : BaseRepository<T>, IRepositoryReadOnly<T> where T : class
    {
        public RepositoryReadOnly(DbContext context,IMapper mapper) : base(context,mapper)
        {
        }
    }
}