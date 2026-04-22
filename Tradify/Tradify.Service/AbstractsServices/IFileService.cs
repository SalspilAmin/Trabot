using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Service.Services;

namespace Tradify.Service.AbstractsServices
{
    public interface IFileService
    {

        public Task<string> UploadFile(string FilePath, IFormFile File);
        public Task<string> UploadGenericAsync(UploadFolder folder, int id, IFormFile file);
        public Task<(string Error, string? Url, string? PublicId)> UploadImageAsync(IFormFile file, string folder);
        public  Task<bool> DeleteImageAsync(string publicId);
        Task DeleteFile(string filePath);

        string GetBaseUrl();
    }
}
