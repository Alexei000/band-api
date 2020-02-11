
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandApi.Entities
{
    public class Band
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime Founded { get; set; }

        [Required]
        [MaxLength(50)]
        public string MainGenre { get; set; }


        public ICollection<Album> Type { get; set; } = new List<Album>();

    }
}
