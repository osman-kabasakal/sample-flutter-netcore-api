using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public int? PictureId { get; set; }

        public virtual Picture Picture { get; set; }
        public virtual Product Product { get; set; }
    }
}