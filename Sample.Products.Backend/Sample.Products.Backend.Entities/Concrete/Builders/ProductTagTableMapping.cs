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
            builder.HasKey(pt => new {pt.TagId, pt.ProductId});
            
            builder.HasOne(pt => pt.Tag)
                .WithMany(pt => pt.ProductTags)
                .HasForeignKey(pt => pt.TagId);
            
            builder.HasOne(pt => pt.Product)
                .WithMany(pt => pt.ProductTags)
                .HasForeignKey(pt => pt.ProductId);
        }
    }
}