using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Entities.Concrete.Tables
{
    public class Picture:BaseEntity
    {
        
        public string MimeType { get; set; }

        public string SeoFilename { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }

        public bool IsNew { get; set; }
        
        public byte[] BinaryData { get; set; }

        public string VirtualPath { get; set; }
        [JsonIgnore]
        public virtual Brand Brand { get; set; }
        
        [JsonIgnore]
        public virtual Category Category { get; set; }

        // public virtual ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductPicture> ProductPictures { get; set; }
    }
}