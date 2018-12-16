namespace CasinoReports.Web.Api.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    [AttributeUsage(AttributeTargets.Property)]
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSizeBytes;

        public FileSizeAttribute(int maxFileSizeBytes)
        {
            this.maxFileSizeBytes = maxFileSizeBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file == null)
            {
                return ValidationResult.Success;
            }

            if (file.Length > this.maxFileSizeBytes)
            {
                double maxFileSizeMb = Math.Round(this.maxFileSizeBytes / 1024f / 1024f, 2);

                return new ValidationResult($"Maximum allowed file size: {maxFileSizeMb} MB");
            }

            return ValidationResult.Success;
        }
    }
}
