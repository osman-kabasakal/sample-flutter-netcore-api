using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Tag : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}