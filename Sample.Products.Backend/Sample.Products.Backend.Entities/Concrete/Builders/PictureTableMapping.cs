using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class  PictureTableMapping:BaseEntityMapping<Picture>
    {
        public override void UpConfigure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Pictures");
            builder.Property<int>(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
        }
    }
}