using System;
using System.ComponentModel.DataAnnotations;

namespace Proxy.Api
{
    public record FindSongsRequestDto
    {
        [Required]
        public string SongName { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public string? AlbumName { get; set; }
    }
}

