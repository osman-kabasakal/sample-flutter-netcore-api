using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class BrandTableMapping:BaseEntityMapping<Brand>
    {
        public override void UpConfigure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.HasKey( x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PictureId).IsRequired(false);
            builder.HasOne(x => x.Picture).WithOne(x => x.Brand).HasForeignKey<Brand>(x=>x.PictureId);
            builder.HasOne<Product>(x => x.Product).WithOne(x => x.Brand).HasForeignKey<Product>(x => x.BrandId);
        }
    }
}