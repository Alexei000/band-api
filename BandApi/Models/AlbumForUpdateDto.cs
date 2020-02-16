using System.ComponentModel.DataAnnotations;

namespace BandApi.Models
{
    public class AlbumForUpdateDto : AlbumManipulationDto
    {
       [Required(ErrorMessage = "Description is required for updating albums")]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }
    }
}
