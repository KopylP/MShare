using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Proxy.Api.Validators;

namespace Proxy.Api
{
    [MetadataRequestValidation]
    public class SongMetadataRequestDto
    {
        [Url]
        public string? Url { get; set; }

        public string? SongName { get; set; }

        public string? ArtistName { get; set; }
    }
}