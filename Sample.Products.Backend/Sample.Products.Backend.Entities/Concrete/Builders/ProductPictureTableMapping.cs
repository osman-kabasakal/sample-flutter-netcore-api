using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class ProductPictureTableMapping:BaseEntityMapping<ProductPicture>
    {
        public override void UpConfigure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(pt => new {pt.PictureId, pt.ProductId});

            builder.HasOne(pt => pt.Picture)
                .WithMany(pt => pt.ProductPictures)
                .HasForeignKey(pt => pt.PictureId);

            builder.HasOne(pt => pt.Product)
                .WithMany(pt => pt.ProductPictures)
                .HasForeignKey(pt => pt.ProductId);
        }
    }
}