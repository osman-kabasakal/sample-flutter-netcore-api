using System;
using System.Collections;
using System.Collections.Generic;
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
            builder.Property(x => x.ParentId).IsRequired(false);
            builder.Property(x => x.PictureId).IsRequired(false);
            builder.HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentId)
                .IsRequired(false);
            builder.HasOne(x => x.Picture).WithOne(x => x.Category).HasForeignKey<Category>(x => x.PictureId).IsRequired(false);

            // builder
            //     .HasMany<Product>(x => x.Products)
            //     .WithMany(x => x.Categories)
            //     .UsingEntity<ProductCategory>(
            //         x =>
            //             x.HasOne(pt => pt.Product)
            //                 .WithMany(pt => pt.ProductCategories)
            //                 .HasForeignKey(pt => pt.ProductId).OnDelete(DeleteBehavior.Cascade),
            //         x =>
            //             x.HasOne(pt => pt.Category)
            //                 .WithMany(pt => pt.ProductCategories)
            //                 .HasForeignKey(pt => pt.CategoryId)
            //                 .OnDelete(DeleteBehavior.Cascade),
            //         x => x.HasKey(t => new {t.ProductId, t.CategoryId})
            //     );
            // builder.HasData(GetFirstData());
        }

        private IEnumerable<Category> GetFirstData()
        {
            var cats = new List<Category>();
            for (int i = 0; i < 10; i++)
            {
                cats.Add(new Category()
                {
                    Id = i+1,
                    Name = $"Category-{i + 1}",
                    ProductCategories = new List<ProductCategory>()
                    {
                        new ProductCategory()
                        {
                            Id=i+1,
                            CategoryId = i+1,
                            ProductId = i+1,
                            Product = new Product()
                            {
                                Id = i+1,
                                Name = $"product-category-{i + 1}",
                                Description = "Açıklama",
                                ShortDescription = "short description",
                                Price = Convert.ToDecimal("14.99"),
                                Quantity = 5,
                                BrandId = i+1,
                                Brand = new Brand()
                                {
                                    Id=i+1,
                                    Name = $"Brand-{i+1}",
                                }
                            }
                        }
                    }
                });
            }

            return cats;
        }
    }
}