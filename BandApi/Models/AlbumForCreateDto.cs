using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BandApi.ValidationAttributes;

namespace BandApi.Models
{
    public class AlbumForCreateDto : AlbumManipulationDto // : IValidatableObject
    {
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult("The title and the description must be different", 
        //            new [] { nameof(AlbumForCreateDto)});
        //    }
        //}
    }
}
