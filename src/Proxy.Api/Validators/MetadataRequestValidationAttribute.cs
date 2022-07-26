using System;
using System.ComponentModel.DataAnnotations;

namespace Proxy.Api.Validators
{
    internal class MetadataRequestValidationAttribute : ValidationAttribute
    {
        public MetadataRequestValidationAttribute()
        {
            ErrorMessage = "You should provide at least one parameter!";
        }

        public override bool IsValid(object? value)
        {
            if (value is not null)
            {
                var metadataRequest = value as SongMetadataRequestDto;

                return
                    !string.IsNullOrWhiteSpace(metadataRequest?.Url)
                    || (!string.IsNullOrWhiteSpace(metadataRequest?.ArtistName)
                        && !string.IsNullOrWhiteSpace(metadataRequest?.SongName));
            }

            return false;
        }
    }
}

