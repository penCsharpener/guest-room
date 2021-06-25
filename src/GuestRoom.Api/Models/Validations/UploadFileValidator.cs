using FluentValidation;

namespace GuestRoom.Api.Models.Validations
{
    public class UploadFileValidator : AbstractValidator<ImageApiModel>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x.Location).Must(l => string.IsNullOrWhiteSpace(l)).WithMessage(nameof(ImageApiModel.Location).ToLower());
            RuleFor(x => x.Name).Must(n => string.IsNullOrWhiteSpace(n)).WithMessage(nameof(ImageApiModel.Name).ToLower());
            RuleFor(x => x.Description).Must(d => string.IsNullOrWhiteSpace(d)).WithMessage(nameof(ImageApiModel.Description).ToLower());
            RuleFor(x => x.Path).Must(p => string.IsNullOrWhiteSpace(p)).WithMessage(nameof(ImageApiModel.Path).ToLower());
        }
    }
}
