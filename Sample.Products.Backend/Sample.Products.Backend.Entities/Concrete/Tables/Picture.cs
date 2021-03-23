using System.Collections.Generic;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Picture:BaseEntity
    {
        public int Id { get; set; }
        public string MimeType { get; set; }

        public string SeoFilename { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }

        public bool IsNew { get; set; }

        public string VirtualPath { get; set; }
        public virtual Brand Brand { get; set; }
        
        public byte[] BinaryData { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
    }
}