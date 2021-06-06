using GuestRoom.Domain.Validation;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GuestRoom.Api.Controllers.Settings.Upload
{
    public class ImageUploadApiModel
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        [ValidateFileType("png,jpg,jpeg,webp,bmp")]
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        internal byte[] ToRawFile()
        {
            using (var ms = new MemoryStream())
            {
                File.CopyTo(ms);

                return ms.ToArray();
            }
        }
    }
}