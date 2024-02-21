using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Udemy.Api.Business;
using Udemy.Api.Data.VO;

namespace Udemy.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;
        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        /// <summary>
        /// Download File
        /// </summary>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(byte[]))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            try
            {
                byte[] buffer = _fileBusiness.GetFile(fileName);

                if (buffer != null)
                {
                    HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                    HttpContext.Response.Headers.Append("content-length", buffer.Length.ToString());
                    await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                }

                return new ContentResult();
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("uploadFile")]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(FileDetailVO))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            try
            {
                FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);

                return new OkObjectResult(detail);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Upload Multiple Files
        /// </summary>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(FileDetailVO))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            try
            {
                List<FileDetailVO> details = await _fileBusiness.SaveFilesToDisk(files);

                return new OkObjectResult(details);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
