using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/File")]
    [ApiController]
    public class FileController : ControllerBase
    {
        FileExtensionContentTypeProvider fileExtensionContentTypeProvider;
        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        [HttpGet("{fileId}")]
        public ActionResult Getfile(string fileId)
        {
            string filePath = "33129.rar";

            if (!System.IO.File.Exists(filePath))
            {
                return  NotFound(); 
            }
            var bytes = System.IO.File.ReadAllBytes(filePath);

            //type file be sorat dynamic shenakhte mishavad.
            if(!fileExtensionContentTypeProvider.TryGetContentType(filePath,out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(bytes, contentType, Path.GetFileName(filePath)); 
        }
    }
}
