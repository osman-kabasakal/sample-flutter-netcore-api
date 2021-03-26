namespace Sample.Products.Backend.Business.Concrete.Models.ExportModels
{
    
    public class PictureExportModel
    {
        public int Id { get; set; }
        public string MimeType { get; set; }

        public string SeoFilename { get; set; }

        public string AltAttribute { get; set; }

        public string TitleAttribute { get; set; }

        public bool IsNew { get; set; }
        
        // public string BinaryData { get; set; }

        public string VirtualPath { get; set; }
    }
}