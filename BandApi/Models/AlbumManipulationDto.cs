using System.ComponentModel.DataAnnotations;
using BandApi.ValidationAttributes;

namespace BandApi.Models
{
    [TitleAndDescription(ErrorMessage = "Title must be different...")]

    public abstract class AlbumManipulationDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(400)]
        public virtual string Description { get; set; }
    }
}
