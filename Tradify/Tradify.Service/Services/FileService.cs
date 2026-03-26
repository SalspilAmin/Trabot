using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        Variants
    }
    public class FileService : IFileService

    {
        #region Fildes
        private readonly IWebHostEnvironment webHostEnvironment;
        #endregion

        #region Constructor
        public FileService(IWebHostEnvironment webHostEnvironment)
        {

            this.webHostEnvironment = webHostEnvironment;   
        }

        #endregion

        #region Methods
        public async  Task<string> UploadFile(string FilePath, IFormFile File)
        {

             // extension
            var extension = Path.GetExtension(File.FileName).ToLower();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
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

        public async Task<string> UploadGenericAsync (UploadFolder folder, int id , IFormFile file)
        {
            // 1️⃣ Check file
            if (file == null || file.Length == 0)
                return "NoFile";

            // 2️⃣ Validate extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
                return "InvalidImageType";

            // 3️⃣ Create folder
            var foldrName = folder.ToString().ToLower();
            var folderPath = Path.Combine("uploads" , foldrName, id.ToString());
            var fullFolderPath = Path.Combine(webHostEnvironment.WebRootPath, folderPath);

            if (!Directory.Exists(fullFolderPath))
                Directory.CreateDirectory(fullFolderPath);

            // 4️⃣ Generate unique name
            var fileName = $"{Guid.NewGuid():N}{extension}";

            // 5️⃣ Save file
            var fullPath = Path.Combine(fullFolderPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // 6️⃣ Return path (DB)
            return $"/{folderPath}/{fileName}";
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





        #endregion

    }
}
