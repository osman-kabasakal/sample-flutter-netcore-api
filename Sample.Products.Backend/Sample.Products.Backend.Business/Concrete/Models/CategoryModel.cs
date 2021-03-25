using System.Collections.Generic;

namespace Sample.Products.Backend.Api.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            SubCategories = new List<CategoryModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? PictureId { get; set; }


        // public  PictureModel Picture { get; set; }
        public  CategoryModel ParentCategory { get; set; }
        public  ICollection<CategoryModel> SubCategories { get; set; }
    }
}