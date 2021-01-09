using System.IO;
using GuestRoom.Domain.Validation;
using Microsoft.AspNetCore.Http;

namespace GuestRoom.Api.Controllers.Settings.Upload
{
    public class ImageUploadApiModel
    {
        public int Id { get; set; }

        public string ImageName { get; set; }

        [ValidateFileType("png,jpg,jpeg,webp,bmp")]
        public IFormFile File { get; set; }

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