using System.ComponentModel.DataAnnotations;

namespace Proxy.Api
{
    public record GetByUrlRequestDto
    {
        [Url]
        public string Url { get; set; }

        public string Region { get; set; }

        public static GetByUrlRequestDto Of(string url, string region) => new GetByUrlRequestDto
        {
            Url = url,
            Region = region
        };
    } 
}