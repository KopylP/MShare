using System;
using MShare.Framework.Application.Validation;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Domain.Specifications;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
    public class QueryValidator : IRequestValidator<GetSongByUrlQuery, SongResponseDto>
    {
        public void Validate(GetSongByUrlQuery request)
        {
            var isValidUrl = ValidSongUrlSpecification.Instance.IsSatisfiedBy(new Uri(request.SongUrl));
            BadRequestException.ThrowIf(!isValidUrl, "Url is not valid");
        }
    }
}