using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public abstract class BaseEntityMapping<TEntity>:IEntityTypeConfiguration<TEntity>
    where TEntity:BaseEntity,IEntity,new()
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.TimeStamp).IsRowVersion();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            UpConfigure(builder);
        }
        
        public abstract void UpConfigure(EntityTypeBuilder<TEntity> builder);
    }
}