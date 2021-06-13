using GuestRoom.Domain.Models.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace GuestRoom.Domain.Models.Configurations
{
    public class TextModelConfiguration : IEntityTypeConfiguration<TextModel>
    {
        public void Configure(EntityTypeBuilder<TextModel> builder)
        {
            builder.HasData(new TextModel[] {
                new() { Id = 1, TextModelType = TextModelTypes.Legal, JsonText = ToJson<LegalModel>() },
                new() { Id = 2, TextModelType = TextModelTypes.LegalParagraph, JsonText = ToJson<LegalParagraphModel>() },
                new() { Id = 3, TextModelType = TextModelTypes.Contact, JsonText = ToJson<ContactModel>() },
                new() { Id = 4, TextModelType = TextModelTypes.Home, JsonText = ToJson<HomeModel>() },
                new() { Id = 5, TextModelType = TextModelTypes.ContentBase, JsonText = ToJson<ContentBaseModel>() },
                new() { Id = 6, TextModelType = TextModelTypes.Liability, JsonText = ToJson<LiabilityModel>() },
                new() { Id = 7, TextModelType = TextModelTypes.Miscellaneous, JsonText = ToJson<MiscellaneousModel>() },
                new() { Id = 8, TextModelType = TextModelTypes.Room1, JsonText = ToJson<RoomModel>() },
                new() { Id = 9, TextModelType = TextModelTypes.Room2, JsonText = ToJson<RoomModel>() },
            });
        }

        public static string ToJson<T>() where T : new()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            return JsonSerializer.Serialize(new T(), options);
        }
    }
}
