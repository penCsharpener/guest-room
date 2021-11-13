using FluentValidation;
using GuestRoom.Api.Controllers.Settings.Upload;

namespace GuestRoom.Api.Models.Validations
{
    public class UploadFileValidator : AbstractValidator<ImageUploadApiModel>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x.Location).Must(l => string.IsNullOrWhiteSpace(l)).WithMessage(nameof(ImageUploadApiModel.Location).ToLower());
            RuleFor(x => x.ImageName).Must(n => string.IsNullOrWhiteSpace(n)).WithMessage(nameof(ImageUploadApiModel.ImageName).ToLower());
            RuleFor(x => x.Description).Must(d => string.IsNullOrWhiteSpace(d)).WithMessage(nameof(ImageUploadApiModel.Description).ToLower());
            RuleFor(x => x.Id).Must(p => p > 0).WithMessage(nameof(ImageUploadApiModel.Id).ToLower());
        }
    }
}
