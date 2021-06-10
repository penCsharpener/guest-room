namespace GuestRoom.Domain.Models.Content
{
    public class LegalModel : ContentBaseModel
    {
        public ContactModel Contact { get; set; }
        public LegalParagraphModel[] LegalParagraphs { get; set; }
        public LegalRequirementsModel LegalRequirements { get; set; }
    }
}