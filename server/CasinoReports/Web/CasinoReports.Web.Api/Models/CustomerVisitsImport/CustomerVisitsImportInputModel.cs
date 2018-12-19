namespace CasinoReports.Web.Api.Models.CustomerVisitsImport
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CasinoReports.Infrastructure.Data.Abstractions.Repositories;
    using CasinoReports.Web.Api.ValidationAttributes;

    using Microsoft.AspNetCore.Http;

    using FileExtensionsAttribute = CasinoReports.Web.Api.ValidationAttributes.FileExtensionsAttribute;

    public class CustomerVisitsImportInputModel : IValidatableObject
    {
        [Required]
        public int CustomerVisitsCollectionId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [FileExtensions(".csv")]
        [FileMimeTypes(
            "text/csv",
            "text/x-csv",
            "application/csv",
            "application/x-csv",
            "text/comma-separated-values",
            "text/x-comma-separated-values",
            "application/vnd.ms-excel")]
        [FileSize(2 * 1024 * 1024)]
        public IFormFile CustomerVisits { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var repository = validationContext.GetService(typeof(ICustomerVisitsCollectionRepository))
                as ICustomerVisitsCollectionRepository;
            if (repository == null)
            {
                yield return ValidationResult.Success;
            }

            var item = repository.GetByIdAsync(this.CustomerVisitsCollectionId).GetAwaiter().GetResult();
            if (item == null)
            {
                yield return new ValidationResult(
                    $"Customer visits collection with ID {this.CustomerVisitsCollectionId} does not exist");
            }

            yield return ValidationResult.Success;
        }
    }
}
