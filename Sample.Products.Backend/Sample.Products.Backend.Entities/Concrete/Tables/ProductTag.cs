using System.Text.Json.Serialization;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class ProductTag:BaseEntity
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Tag Tag { get; set; }
    }
}