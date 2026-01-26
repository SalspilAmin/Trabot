using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tradify.Service.AbstractsServices;

namespace Tradify.Service.Services
{
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
            var fileName = new Guid().ToString().Replace("-",string.Empty)+extension;

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
        #endregion
        
    }
}
