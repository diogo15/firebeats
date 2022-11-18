using Firebeats.Uploads.Interfaces;
using Microsoft.Extensions.Hosting.Internal;

namespace Firebeats.Uploads.Services
{
    public class FileUploadLocalService : IBufferedFileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadLocalService(IWebHostEnvironment environment)
        {
            this._environment = environment;
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(_environment.WebRootPath, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
