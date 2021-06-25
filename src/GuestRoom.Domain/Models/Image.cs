using System;

namespace GuestRoom.Domain.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime UploadedOn { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}
