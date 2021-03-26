using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Products.Backend.Entities.Concrete.Tables;

namespace Sample.Products.Backend.Entities.Concrete.Builders
{
    public class CustomerTableMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
        }
    }
}