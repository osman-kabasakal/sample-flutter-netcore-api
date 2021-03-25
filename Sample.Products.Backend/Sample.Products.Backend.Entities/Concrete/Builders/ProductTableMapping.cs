using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class ProductTableMapping:BaseEntityMapping<Product>
    {
        public override void UpConfigure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasOne(x => x.Brand).WithOne(x => x.Product).HasForeignKey<Product>(x => x.BrandId);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            
            // builder.HasMany(pt => pt.ProductPictures)
            //     .WithOne(pt => pt.Product)
            //     .HasForeignKey(pt => pt.ProductId);
            
            // builder.HasMany(x => x.ProductTags)
            //     .WithOne(x => x.Product)
            //     .HasForeignKey(x => x.ProductId);
            
            // builder.HasMany<Picture>(x => x.Pictures).WithMany(x => x.Products)
            //     .UsingEntity<ProductPicture>(x =>
            //             x.HasOne(pt => pt.Picture)
            //                 .WithMany(pt => pt.ProductPictures)
            //                 .HasForeignKey(pt => pt.PictureId),
            //         x =>
            //             x.HasOne(pt => pt.Product)
            //                 .WithMany(pt => pt.ProductPictures)
            //                 .HasForeignKey(pt => pt.ProductId),
            //         x => x.HasKey(pt => new {pt.PictureId, pt.ProductId})
            //     );
            // builder.HasMany(x => x.Tags).WithMany(x => x.Products)
            //     .UsingEntity<ProductTag>(x =>
            //             x.HasOne(pt => pt.Tag)
            //                 .WithMany(pt => pt.ProductTags)
            //                 .HasForeignKey(pt => pt.TagId),
            //         x =>
            //             x.HasOne(pt => pt.Product)
            //                 .WithMany(pt => pt.ProductTags)
            //                 .HasForeignKey(pt => pt.ProductId),
            //         x =>
            //             x.HasKey(pt => new {pt.TagId, pt.ProductId})
            //     );
        }
    }
}