using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        // public virtual ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}