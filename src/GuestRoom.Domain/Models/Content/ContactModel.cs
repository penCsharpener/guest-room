namespace GuestRoom.Domain.Models.Content
{
    public class ContactModel : ContentBaseModel
    {
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string StreetAndHouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}