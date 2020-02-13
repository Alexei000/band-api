using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Models
{
    public class BandForCreateDto
    {
        public string Name { get; set; }
        public DateTime Founded { get; set; }
        public string MainGenre { get; set; }

        public ICollection<AlbumForCreateDto> Albums { get; set; } = new List<AlbumForCreateDto>();

    }
}
