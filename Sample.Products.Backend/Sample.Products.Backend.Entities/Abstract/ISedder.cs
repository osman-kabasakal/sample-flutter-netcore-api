using System.Collections.Generic;

namespace Sample.Products.Backend.Entities.Abstract
{
    public interface ISeeder
    {
        List<T> GetSeedData<T>()
            where T:class,IEntity,new();
    }
}