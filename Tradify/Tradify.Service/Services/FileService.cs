using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradify.Service.AbstractsServices;

namespace Tradify.Service.Services
{
    public enum UploadFolder
    {
        Products,
        Variants,
        Store,
        Instructor
            ,Post
            ,Message
    }
 
    public class FileService : IFileService

    {
        #region Fildes
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Cloudinary cloudinary;

        #endregion

        #region Constructor
        public FileService(IWebHostEnvironment webHostEnvironment , IHttpContextAccessor httpContextAccessor , IConfiguration config)
        {

            this.webHostEnvironment = webHostEnvironment;  
            this.httpContextAccessor = httpContextAccessor;
            var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );

            cloudinary = new Cloudinary(account);
        }
        

        #endregion

        #region Methods
        public async  Task<string> UploadFile(string FilePath, IFormFile File)
        {

             // extension
            var extension = Path.GetExtension(File.FileName).ToLower();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp"  };
            if (!allowedExtensions.Contains(extension))
                return "InvalidImageType";
            // path
            var folderPath = Path.Combine(
             webHostEnvironment.WebRootPath,
            FilePath
         );




            //fileName
            var fileName = Guid.NewGuid().ToString("N") + extension;

            if (File.Length >0)
            {
                try 
                {

                    if (!Directory.Exists(folderPath))
                    {

                        Directory.CreateDirectory(folderPath);
                    }
                    var fullPath = Path.Combine(folderPath, fileName);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await File.CopyToAsync(stream);

                    return $"/{FilePath}/{fileName}";
                }
                catch (Exception ex)
                
                {
                    return "FailedToUploadImage";
                }

            }
            else
            {
                return "NoImage";
            }
            
        }


        // For cloudinary

        public async Task<(string Error, string? Url, string? PublicId)> UploadImageAsync(IFormFile file,  string folder)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return ("NoFile",null, null);

                var extension = Path.GetExtension(file.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

                if (!allowedExtensions.Contains(extension))
                    return ("InvalidImageType",null, null);

                var maxSize = 5 * 1024 * 1024;
                if (file.Length > maxSize)
                    return ("FileTooLarge", null, null);

                if (string.IsNullOrEmpty(file.ContentType) || !file.ContentType.StartsWith("image/"))
                    return ("InvalidFileType", null, null);

                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = folder
                };

                var result = await cloudinary.UploadAsync(uploadParams);

                if (result.Error != null)
                    return ("FailedToUploadImage", null, null);

                return ("Success", result.SecureUrl.ToString(), result.PublicId);
            }
            catch
            {
                return ("FailedToUploadImage", null, null);
            }
        }

        // For cloudinary

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
                return false;

            var deleteParams = new DeletionParams(publicId);

            var result = await cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }



        public async Task<string> UploadGenericAsync(UploadFolder folder, int id, IFormFile file)
        {
            try
            {
                // 1. Check file
                if (file == null || file.Length == 0)
                    return "NoFile";

                // 2. Validate extension
                var extension = Path.GetExtension(file.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

                if (!allowedExtensions.Contains(extension))
                    return "InvalidImageType";

                // 3. Validate size
                var maxSize = 5 * 1024 * 1024; // 5MB
                if (file.Length > maxSize)
                    return "FileTooLarge";

                // 4. Validate content type
                if (string.IsNullOrEmpty(file.ContentType) || !file.ContentType.StartsWith("image/"))
                    return "InvalidFileType";


                // 5. Create folder
                var folderName = folder.ToString().ToLower();
                var folderPath = Path.Combine("uploads", folderName, id.ToString());
                var fullFolderPath = Path.Combine(webHostEnvironment.WebRootPath, folderPath);

                Directory.CreateDirectory(fullFolderPath);

                // 6. Generate unique name
                var fileName = $"{Guid.NewGuid():N}{extension}";

                // 7. Save file
                var fullPath = Path.Combine(fullFolderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);

                // 8. Return clean path
                return $"/{folderPath.Replace("\\", "/")}/{fileName}";
            }
            catch
            {
                return "FailedToUploadImage";
            }
        }

        public Task DeleteFile(string filePath)
        {
            var fullPath = Path.Combine(webHostEnvironment.WebRootPath, filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }

        public string GetBaseUrl()
        {
            var request = httpContextAccessor.HttpContext.Request;
            if (request == null)
                return ""; // أو return default URL
            return $"{request.Scheme}://{request.Host}";
        }



        #endregion

    }
}
