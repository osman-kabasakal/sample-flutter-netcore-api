using System.Collections.Generic;

namespace Sample.Products.Backend.Api.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            Tags = new List<TagModel>();
            // Pictures = new List<PictureModel>();
            // Categories = new List<CategoryModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }

        public BrandModel Brand { get; set; }
        public  ICollection<TagModel> Tags { get; set; }
        // public  ICollection<PictureModel> Pictures { get; set; }
        public ICollection<int> CategoryIds { get; set; }
        public ICollection<int> PictureIds { get; set; }

        // public  ICollection<CategoryModel> Categories { get; set; }
    }
}