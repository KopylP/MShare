using System;
using MShare.Framework.Application.Actions;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.Queries.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Domain.Specifications;

namespace MShare.Songs.Application.Queries.V1.GetByUrl
{
    public class QueryValidator : IRequestValidator<GetByUrlQuery, GetByUrlResponseDto>
    {
        public async Task Validate(GetByUrlQuery request)
        {
            var isValidUrl = ValidUrlSpecification.Instance.IsSatisfiedBy(new Uri(request.Url));
            BadRequestException.ThrowIf(!isValidUrl, "Url is not valid");

            await Task.CompletedTask;
        }
    }
}