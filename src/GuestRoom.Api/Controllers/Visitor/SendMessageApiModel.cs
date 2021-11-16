namespace GuestRoom.Api.Controllers.Contact
{
    public class SendMessageApiModel
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
    }
}
