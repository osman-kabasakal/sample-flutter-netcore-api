using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy.Internal;
using Microsoft.EntityFrameworkCore;
using Sample.Products.Backend.Entities.Abstract;
using Sample.Products.Backend.Entities.Concrete.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.DataAccess.Concrete.EntityFramework.Context
{
    public class SampleProductsContext:BaseContext
    {
        public SampleProductsContext():base()
        {
            SetDb();
        }
        
        public SampleProductsContext(DbContextOptions options):base(options)
        {
            SetDb();
        }
        
        private  const string EntitiesNameSpace = "Sample.Products.Backend.Entities.Concrete.Tables";
        private const string EntityMapsNameSpace = "Sample.Products.Backend.Entities.Concrete.Builders";
        private IEnumerable<Type> _entities;
        private void SetDb()
        {
            _entities = from t in (Assembly.GetAssembly(typeof(BaseEntity))?.GetTypes())
                where t.IsClass && t.Namespace == EntitiesNameSpace
                                && t.GetAllInterfaces().Select(x => x.ToString()).Contains(typeof(IEntity).ToString())
                select t;
            Console.WriteLine("SetDb Çalıştı");
            foreach (var Entity in _entities)
            {
                Console.WriteLine(Entity.Name);
                MethodInfo method = this.GetType().GetMethod("Set",new Type[]{});
                MethodInfo generic = method.MakeGenericMethod(Entity);
                generic.Invoke(this, null);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var map = Assembly.GetAssembly(typeof(BaseEntityMapping<>))?.GetTypes().FirstOrDefault(x => x.Namespace==EntityMapsNameSpace);
            if(map!= null)
             modelBuilder.ApplyConfigurationsFromAssembly(map.Assembly);
        }
    }
}