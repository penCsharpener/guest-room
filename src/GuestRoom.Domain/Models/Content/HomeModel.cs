namespace GuestRoom.Domain.Models.Content
{
    public class HomeModel : ContentBaseModel
    {
        public string PageTitle { get; set; }
        public string WelcomeParagraph { get; set; }
        public string MapsLink { get; set; }
    }
}