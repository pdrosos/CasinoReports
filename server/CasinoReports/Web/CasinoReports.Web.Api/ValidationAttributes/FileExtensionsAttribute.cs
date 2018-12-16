namespace CasinoReports.Web.Api.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    [AttributeUsage(AttributeTargets.Property)]
    public class FileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] allowedFileExtensions;

        public FileExtensionsAttribute(params string[] allowedFileExtensions)
        {
            this.allowedFileExtensions = allowedFileExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file == null)
            {
                return ValidationResult.Success;
            }

            string fileExtension = Path.GetExtension(file.FileName);
            if (!this.allowedFileExtensions.Contains(fileExtension, StringComparer.CurrentCultureIgnoreCase))
            {
                return new ValidationResult(
                    $"Allowed file extensions: {string.Join(", ", this.allowedFileExtensions)}");
            }

            return ValidationResult.Success;
        }
    }
}
