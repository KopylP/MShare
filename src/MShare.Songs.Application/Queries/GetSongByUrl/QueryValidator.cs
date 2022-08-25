using System;
using MShare.Framework.Application.Validation;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Api.Queries;
using MShare.Songs.Api.Queries.Dtos;
using MShare.Songs.Domain.Specifications;

namespace MShare.Songs.Application.Queries.GetSongByUrl
{
    public class QueryValidator : IRequestValidator<GetSongByUrlQuery, SongByUrlResponseDto>
    {
        public void Validate(GetSongByUrlQuery request)
        {
            var isValidUrl = ValidSongUrlSpecification.Instance.IsSatisfiedBy(new Uri(request.SongUrl));
            BadRequestException.ThrowIf(!isValidUrl, "Url is not valid");
        }
    }
}

