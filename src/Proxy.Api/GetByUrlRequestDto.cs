using System.ComponentModel.DataAnnotations;

namespace Proxy.Api
{
    public record GetByUrlRequestDto
    {
        [Url]
        [Required]
        public string Url { get; set; }

        public static GetByUrlRequestDto Of(string url) => new GetByUrlRequestDto
        {
            Url = url
        };
    }
}