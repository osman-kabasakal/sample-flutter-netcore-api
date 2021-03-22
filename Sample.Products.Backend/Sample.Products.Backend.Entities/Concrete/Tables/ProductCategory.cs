using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class ProductCategory : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}