using System.Text.Json.Serialization;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class ProductPicture : BaseEntity
    {
        public int PictureId { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public virtual Picture Picture { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}