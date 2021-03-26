using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Sample.Products.Backend.Entities.Abstract;

namespace Sample.Products.Backend.Business.Concrete.Services
{
    public class FileService:PhysicalFileProvider,IFileService
    {
        
        public FileService(IWebHostEnvironment webHostEnvironment) : 
            base(File.Exists(webHostEnvironment.ContentRootPath) ? 
                Path.GetDirectoryName(webHostEnvironment.ContentRootPath) : webHostEnvironment.ContentRootPath)
        {
            WebRootPath = File.Exists(webHostEnvironment.WebRootPath)
                ? Path.GetDirectoryName(webHostEnvironment.WebRootPath)
                : webHostEnvironment.WebRootPath;
        }
        protected static bool IsUncPath(string path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out var uri) && uri.IsUnc;
        }
        
        public virtual string Combine(params string[] paths)
        {
            var path = Path.Combine(paths.SelectMany(p => IsUncPath(p) ? new[] { p } : p.Split('\\', '/')).ToArray());
            if (Environment.OSVersion.Platform == PlatformID.Unix && !IsUncPath(path))
                path = "/" + path;

            return path;
        }
        
        public virtual void WriteAllBytes(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }
        
        public virtual byte[] ReadAllBytes(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllBytes(filePath) : Array.Empty<byte>();
        }
        
        public virtual string GetAbsolutePath(params string[] paths)
        {
            var allPaths = new List<string>();

            if(paths.Any() && !paths[0].Contains(WebRootPath, StringComparison.InvariantCulture))
                allPaths.Add(WebRootPath);

            allPaths.AddRange(paths);

            return Combine(allPaths.ToArray());
        }

        public string GetVirtualPath(string path)
        {
            if (path.Contains(WebRootPath, StringComparison.InvariantCulture))
            {
                return path.Replace(WebRootPath, "");
            }
            return path;
        }


        protected string WebRootPath { get; }
    }


    public interface IFileService:IFileProvider
    {
        string Combine(params string[] paths);
        void WriteAllBytes(string filePath, byte[] bytes);
        byte[] ReadAllBytes(string filePath);

        string GetAbsolutePath(params string[] paths);

        string GetVirtualPath(string path);
    }
}