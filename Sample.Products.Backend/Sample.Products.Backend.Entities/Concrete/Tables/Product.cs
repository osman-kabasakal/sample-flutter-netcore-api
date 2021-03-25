using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }

        [JsonIgnore]
        public virtual Brand Brand { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        // public virtual ICollection<Tag> Tags { get; set; }
        // public virtual ICollection<Picture> Pictures { get; set; }
        // public virtual ICollection<Category> Categories { get; set; }
        
    }
}