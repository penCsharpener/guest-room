using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestRoom.Domain.Models.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Location).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Path).IsRequired();
        }

    }
}
