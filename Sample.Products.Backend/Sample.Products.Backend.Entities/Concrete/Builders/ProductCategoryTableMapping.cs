using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class ProductCategoryTableMapping:BaseEntityMapping<ProductCategory>
    {
        public override void UpConfigure(EntityTypeBuilder<ProductCategory> builder)
        {
            // builder.ToTable("ProductCategories");
            builder.HasKey(t => new {t.ProductId, t.CategoryId});
            
            builder.HasOne(pt => pt.Product)
                .WithMany(pt => pt.ProductCategories)
                .HasForeignKey(pt => pt.ProductId).OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(pt => pt.Category)
                .WithMany(pt => pt.ProductCategories)
                .HasForeignKey(pt => pt.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}