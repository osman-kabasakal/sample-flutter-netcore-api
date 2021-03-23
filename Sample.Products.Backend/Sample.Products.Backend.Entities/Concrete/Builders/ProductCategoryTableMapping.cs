using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class ProductCategoryTableMapping:BaseEntityMapping<ProductCategory>
    {
        public override void UpConfigure(EntityTypeBuilder<ProductCategory> builder)
        {
            
        }
    }
}