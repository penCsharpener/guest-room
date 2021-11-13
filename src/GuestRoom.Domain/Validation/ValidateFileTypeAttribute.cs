using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

#nullable enable

namespace GuestRoom.Domain.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateFileTypeAttribute : ValidationAttribute
    {
        private readonly string[] _validTypes;

        public ValidateFileTypeAttribute(params string[] validTypes)
        {
            _validTypes = validTypes;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new("Cannot be null.");
            }

            var fileName = ((IFormFile)value).FileName;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return new("File name cannot be null, empty or white space.");
            }

            if (!fileName.Contains('.'))
            {
                return new("Filename must contain a file extension.");
            }

            var fileExtension = Path.GetExtension(fileName).Replace(".", "");

            if (_validTypes.Contains(fileExtension))
            {
                return ValidationResult.Success;
            }

            return new($"File extensions '{fileExtension}' is not valid.");
        }
    }
}