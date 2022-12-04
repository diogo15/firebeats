using Firebeats.Uploads.Interfaces;
using Firebeats.Uploads.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Firebeats.Uploads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IBufferedFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _environment;
        AnalizeSong analizer = new AnalizeSong();

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
                    return StatusCode(StatusCodes.Status200OK, "/UploadedFiles/" + file.FileName);
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

        [HttpGet("CreateDataGraph")]
        public async Task<ActionResult> CreateDataGraph(string name)
        {
            
            try
            {

                var data = await analizer.CreateDataGraph(getFilePath(name));
                return StatusCode(StatusCodes.Status200OK, data);

            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "auch");

            }

        }

        [HttpGet("CreateGraph")]
        public async Task<ActionResult> CreateGraph(string name)
        {

            try
            {

                await analizer.CreateGraph(getFilePath(name));
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
