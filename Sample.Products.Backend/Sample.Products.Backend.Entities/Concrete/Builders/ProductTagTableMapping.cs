using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class ProductTagTableMapping:BaseEntityMapping<ProductTag>
    {
        public override void UpConfigure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("ProductTags");
        }
    }
}