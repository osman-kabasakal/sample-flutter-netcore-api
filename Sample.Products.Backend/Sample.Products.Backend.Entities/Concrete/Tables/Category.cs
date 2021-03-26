using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? PictureId { get; set; }


        [JsonIgnore]
        public virtual Picture Picture { get; set; }
        [JsonIgnore]
        public virtual Category ParentCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<Category> SubCategories { get; set; }

        // public virtual ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}