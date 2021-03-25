using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class ProductPicture : BaseEntity
    {
        public int PictureId { get; set; }
        public int ProductId { get; set; }

        public virtual Picture Picture { get; set; }
        public virtual Product Product { get; set; }
    }
}