using System.ComponentModel.DataAnnotations;
using BandApi.Models;

namespace BandApi.ValidationAttributes
{
    public class TitleAndDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var album = (AlbumManipulationDto) validationContext.ObjectInstance;
            if (album.Title == album.Description)
                return new ValidationResult("The title and the description must be different", 
                    new [] { nameof(AlbumManipulationDto) });

            return ValidationResult.Success;
        }
    }
}
