using System.ComponentModel.DataAnnotations;

namespace Proxy.Api
{
    public record GetSongByUrlRequestDto
    {
        [Url]
        [Required]
        public string Url { get; set; }
    }
}