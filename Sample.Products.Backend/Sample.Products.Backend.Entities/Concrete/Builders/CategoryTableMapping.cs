using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class CategoryTableMapping : BaseEntityMapping<Category>
    {
        public override void UpConfigure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.ParentId).IsRequired(false);
            builder.HasMany(x => x.SubCategories).WithOne(x => x.ParentCategory).HasForeignKey(x=>x.ParentId).IsRequired(false);
            builder.HasOne(x => x.Picture).WithOne(x => x.Category).HasForeignKey<Category>(x => x.PictureId);
            
            builder
                .HasMany<Product>(x => x.Products)
                .WithMany(x => x.Categories)
                .UsingEntity<ProductCategory>(
                    x =>
                        x.HasOne(pt => pt.Product)
                            .WithMany(pt => pt.ProductCategories)
                            .HasForeignKey(pt => pt.ProductId).OnDelete(DeleteBehavior.Cascade),
                    x =>
                        x.HasOne(pt => pt.Category)
                            .WithMany(pt => pt.ProductCategories)
                            .HasForeignKey(pt => pt.CategoryId)
                            .OnDelete(DeleteBehavior.Cascade),
                    x => x.HasKey(t => new {t.ProductId, t.CategoryId})
                );
        }
    }
}