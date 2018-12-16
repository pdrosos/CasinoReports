namespace CasinoReports.Web.Api.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    [AttributeUsage(AttributeTargets.Property)]
    public class FileMimeTypesAttribute : ValidationAttribute
    {
        private readonly string[] allowedMimeTypes;

        public FileMimeTypesAttribute(params string[] allowedMimeTypes)
        {
            this.allowedMimeTypes = allowedMimeTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file == null)
            {
                return ValidationResult.Success;
            }

            if (!this.allowedMimeTypes.Contains(file.ContentType, StringComparer.CurrentCultureIgnoreCase))
            {
                return new ValidationResult(
                    $"Allowed MIME types: {string.Join(", ", this.allowedMimeTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
