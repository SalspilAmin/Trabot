using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Service.AbstractsServices
{
    public interface IFileService
    {

        public Task<string> UploadFile(string FilePath, IFormFile File);

    }
}
