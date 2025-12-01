using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IFileService
    {
         public Task<string>  UploadFileAsync(IFormFile file, string folderName);
            public Task DeleteFileAsync(string filepath);
    }
}
