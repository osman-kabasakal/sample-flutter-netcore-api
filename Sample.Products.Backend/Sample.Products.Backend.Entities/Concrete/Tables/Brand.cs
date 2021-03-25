using System.Text.Json.Serialization;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public int? PictureId { get; set; }

        [JsonIgnore]
        public virtual Picture Picture { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}