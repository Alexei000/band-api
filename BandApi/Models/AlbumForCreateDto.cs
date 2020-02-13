using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BandApi.ValidationAttributes;

namespace BandApi.Models
{
    [TitleAndDescription(ErrorMessage = "Title must be different...")]
    public class AlbumForCreateDto // : IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(400)]
        public string Description { get; set; }

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
