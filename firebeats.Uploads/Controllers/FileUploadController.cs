using Firebeats.Uploads.Interfaces;
using Firebeats.Uploads.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Firebeats.Uploads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IBufferedFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _environment;

        public FileUploadController(IBufferedFileUploadService fileUploadService, IWebHostEnvironment environment)
        {
            _fileUploadService = fileUploadService;
            this._environment = environment;
        }


        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)
        {
            try
            {
                if (await _fileUploadService.UploadFile(file))
                {
                    var path = getFilePath(file.FileName);

                    return StatusCode(StatusCodes.Status200OK, path);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "File Upload Failed");
                }
            }
            catch (Exception ex)
            {
                //Log ex
                return StatusCode(StatusCodes.Status200OK, ex);
            }

        }

        [HttpGet("CreateGraph")]
        public async Task<ActionResult> CreateGraph(string name)
        {
            var path = getFilePath(name);
            var analizer = new AnalizeSong();

            try
            {

                await analizer.CreateGraph(path);
                return StatusCode(StatusCodes.Status200OK, "yuhuu");

            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "auch");

            }

        }


        [NonAction]
        public string getFilePath(string songName)
        {
            return this._environment.WebRootPath + "\\UploadedFiles\\" + songName;
        }
    }
}
