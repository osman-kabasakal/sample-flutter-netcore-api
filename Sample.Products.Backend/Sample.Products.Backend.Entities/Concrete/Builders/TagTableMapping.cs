using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class TagTableMapping:BaseEntityMapping<Tag>
    {
        public override void UpConfigure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
        }
    }
}