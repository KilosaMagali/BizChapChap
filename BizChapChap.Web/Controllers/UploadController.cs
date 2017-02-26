using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BizChapChap.Web.Controllers
{
    [RoutePrefix("api/Upload")]
    public class UploadController : ApiController
    {
            [Route("PostImage")]
            [MimeMultipart]
        public async Task<FileUploadResult> PostImage()
            {

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");

            var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

            // Read the MIME multipart asynchronously 
            await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

            string _localFileName = multipartFormDataStreamProvider
                .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

            foreach (var key in multipartFormDataStreamProvider.FormData.AllKeys)
            {
                foreach (var val in multipartFormDataStreamProvider.FormData.GetValues(key))
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}: {1}", key, val));
                }
            }

            var caption = multipartFormDataStreamProvider.FormData.GetValues("file[Caption]").FirstOrDefault();

            // Create response
            return new FileUploadResult
            {
                LocalFilePath = _localFileName,

                FileName = Path.GetFileName(_localFileName),

                FileLength = new FileInfo(_localFileName).Length,

                Caption = caption
            };
        }


        [HttpGet]
        [Route("GetImage")]
        public FileUploadResult GetImage(string imageName)
        {
            var imagesFolderPath = HttpContext.Current.Server.MapPath("~/Uploads");

            DirectoryInfo directoryInfo = new DirectoryInfo(imagesFolderPath);
            var images = directoryInfo.GetFiles().ToList();

            string _localFileName = images.Where(p => p.Name.ToLower().Contains(imageName.ToLower())).Select(q => q.FullName).FirstOrDefault();

            if (_localFileName == null)
                return null;

            return new FileUploadResult
            {
                LocalFilePath = _localFileName,

                FileName = Path.GetFileName(_localFileName),

                FileLength = new FileInfo(_localFileName).Length
            };
        }

        [HttpGet]
        [Route("GetImages")]
        public IEnumerable<FileUploadResult> GetAllImages()
        {
            var imagesFolderPath = HttpContext.Current.Server.MapPath("~/Uploads");

            DirectoryInfo directoryInfo = new DirectoryInfo(imagesFolderPath);
            var images = directoryInfo.GetFiles().ToList();

            string _localFileName = "";

            if (images.Count == 0)
                return null;

            var listImages = new List<FileUploadResult>();

            foreach(var item in images)
            {
                _localFileName = item.FullName;
                listImages.Add(new FileUploadResult
                {
                    LocalFilePath = _localFileName,

                    FileName = Path.GetFileName(_localFileName),

                    FileLength = new FileInfo(_localFileName).Length
                });
            }

            return listImages;
            
        }
    }

    public class ImageVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FileUploadResult
    {
        public string LocalFilePath { get; set; }
        public string FileName { get; set; }
        public long FileLength { get; set; }
        public string Caption { get; set; }
    }

    public class UploadMultipartFormProvider : MultipartFormDataStreamProvider
    {
        public UploadMultipartFormProvider(string rootPath) : base(rootPath) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null &&
                headers.ContentDisposition != null)
            {
                return headers
                    .ContentDisposition
                    .FileName.TrimEnd('"').TrimStart('"');
            }

            return base.GetLocalFileName(headers);
        }
    }

    public class MimeMultipart : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(
                    new HttpResponseMessage(
                        HttpStatusCode.UnsupportedMediaType)
                );
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }

}
