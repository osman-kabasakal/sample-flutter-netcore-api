using System;
using System.IO;
using System.Linq;
using Sample.Products.Backend.Business.Abstract;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork;
using Sample.Products.Backend.Entities.Concrete.Tables;
using System.Net.Http;
using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Sample.Products.Backend.Api.Models;
using Sample.Products.Backend.Business.Concrete.Models;
using Sample.Products.Backend.DataAccess.Concrete.UnitOfWork.Paging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace Sample.Products.Backend.Business.Concrete.Services
{
    [ResponseCache]
    public class PictureService : BaseService<Picture>, IPictureService
    {
        private readonly HttpClient _client;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public PictureService(IUnitOfWork unitOfWork, HttpClient client, IFileService fileService,IMapper mapper) : base(unitOfWork)
        {
            _client = client;
            _fileService = fileService;
            _mapper = mapper;
        }

        private string GetPicturePathWithoutExtension(Picture picture)
        {
            var filePath = _fileService.Combine("images",
                $"{picture.Id:000000}");
            return filePath;
        }

        private string GetPicturePath(Picture picture, string mimeType)
        {
            var path = GetPicturePathWithoutExtension(picture);
            path += $".{GetFileExtensionFromMimeType(picture.MimeType)}";
            return path;
        }

        private string GetPicturePath(string fileName)
        {
            var path = _fileService.Combine("images", fileName);
            return path;
        }

        public ServiceResponse<IPaginate<Picture>> AllPicture()
        {
            return new ServiceResponse<IPaginate<Picture>>()
            {
                Entity = Repository.GetList(index:0,size:int.MaxValue),
                IsSuccessful = true
            };

        }
        
        
        public void UpdatePicture(Picture picture)
        {
            var filePath = GetPicturePath(picture, picture.MimeType);
            if (File.Exists(filePath))
                return;

            var directory = new FileInfo(_fileService.GetAbsolutePath(filePath));
            if (!Directory.Exists(directory.DirectoryName))
            {
                Directory.CreateDirectory(directory.DirectoryName);
            }

            ;
            picture.VirtualPath = filePath;
            Repository.Update(picture);
            _fileService.WriteAllBytes(_fileService.GetAbsolutePath(filePath), picture.BinaryData);
        }

        public ServiceResponse<IPaginate<Picture>> GetBinaryLessPictures()
        {
            var rt = new ServiceResponse<IPaginate<Picture>>()
            {
                IsSuccessful = true,
                Entity = Repository.GetList(x => x.BinaryData == null || x.BinaryData.Length == 0, index: 0,
                    size: int.MaxValue, disableTracking: false)
            };
            return rt;
        }

        public virtual byte[] EncodeImage<TPixel>(Image<TPixel> image, IImageFormat imageFormat, int? quality = null)
            where TPixel : unmanaged, IPixel<TPixel>
        {
            using var stream = new MemoryStream();
            var imageEncoder = Configuration.Default.ImageFormatsManager.FindEncoder(imageFormat);
            switch (imageEncoder)
            {
                case JpegEncoder jpegEncoder:
                    jpegEncoder.Subsample = JpegSubsample.Ratio444;
                    jpegEncoder.Quality = quality ?? 75;
                    jpegEncoder.Encode(image, stream);
                    break;

                case PngEncoder pngEncoder:
                    pngEncoder.ColorType = PngColorType.RgbWithAlpha;
                    pngEncoder.Encode(image, stream);
                    break;

                case BmpEncoder bmpEncoder:
                    bmpEncoder.BitsPerPixel = BmpBitsPerPixel.Pixel32;
                    bmpEncoder.Encode(image, stream);
                    break;

                case GifEncoder gifEncoder:
                    gifEncoder.Encode(image, stream);
                    break;

                default:
                    imageEncoder.Encode(image, stream);
                    break;
            }

            return stream.ToArray();
        }

        public virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            var parts = mimeType.Split('/');
            var lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }

            return lastPart;
        }

        public void SetPictureBinaryFromFile(Picture picture)
        {
            var fileInfo =
                new FileInfo(_fileService.GetAbsolutePath(GetPicturePathWithoutExtension(picture)) + ".jpeg");
            var files = Directory.GetFiles(fileInfo.DirectoryName, $"{$"{picture.Id:000000}"}.*");
            if (!files.IsNullOrEmpty())
            {
                var filePath = files.First();
                using var existingFile = File.OpenRead(files.First());
                using Image<Rgba32> image = Image.Load<Rgba32>(existingFile, out var format);
                var binary = EncodeImage(image, format, 75);
                picture.MimeType = format.DefaultMimeType;
                picture.VirtualPath = _fileService.GetVirtualPath(filePath);
                picture.BinaryData = binary;
                Repository.Update(picture);
                _unitOfWork.SaveChanges();
            }
        }

        public ServiceResponse<PictureModel> GetPictureById(int id)
        {
            var picture = Repository.Single(x => x.Id == id) ??
                          throw new ArgumentNullException("Repository.Single(x => x.Id == id)");
            var filePath = GetPicturePath(picture, picture.MimeType);
            var absolutePath = _fileService.GetAbsolutePath(filePath);
            if (!File.Exists(absolutePath))
            {
                _fileService.WriteAllBytes(absolutePath,picture.BinaryData);
            }
            else if(picture.BinaryData==null||picture.BinaryData.IsNullOrEmpty())
            {
                SetPictureBinaryFromFile(picture);
            }
            return new ServiceResponse<PictureModel>()
            {
                IsSuccessful = true,
                Entity = _mapper.Map<PictureModel>(picture)
            };
        }
    }

    public interface IPictureService
    {
        void UpdatePicture(Picture picture);

        ServiceResponse<IPaginate<Picture>> GetBinaryLessPictures();

        byte[] EncodeImage<TPixel>(Image<TPixel> image, IImageFormat imageFormat, int? quality = null)
            where TPixel : unmanaged, IPixel<TPixel>;

        void SetPictureBinaryFromFile(Picture picture);
        ServiceResponse<PictureModel> GetPictureById(int id);

        ServiceResponse<IPaginate<Picture>> AllPicture();
    }
}