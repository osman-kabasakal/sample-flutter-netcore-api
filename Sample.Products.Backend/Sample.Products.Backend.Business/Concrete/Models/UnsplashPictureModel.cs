using System.Security.Policy;

namespace Sample.Products.Backend.Business.Concrete.Models
{
    public class UnsplashPictureModel
    {
        public string id { get; set; }
        public UnsplasUrl urls { get; set; }
    }

   public class UnsplasUrl
    {
        public string raw { get; set; }
    }
}