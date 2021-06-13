using GuestRoom.Domain.Models.Content;

namespace GuestRoom.Domain.Models
{
    public class TextModel
    {
        public int Id { get; set; }
        public TextModelTypes TextModelType { get; set; }
        public string JsonText { get; set; }
    }
}
