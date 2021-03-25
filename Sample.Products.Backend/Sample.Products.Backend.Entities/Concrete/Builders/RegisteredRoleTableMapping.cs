using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class RegisteredRoleTableMapping:IEntityTypeConfiguration<RegisteredRole>
    {
        public void Configure(EntityTypeBuilder<RegisteredRole> builder)
        {
            builder.ToTable("RegisteredRoles");
        }
    }
}