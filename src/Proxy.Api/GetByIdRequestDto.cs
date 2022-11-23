using System;
using MShare.Framework.Types.Addresses;

namespace Proxy.Api
{
	public class GetByIdRequestDto
	{
		public string Id { get; set; }
		public string Region { get; set; } 


        public static GetByIdRequestDto Of(string Id, string region)
			=> new GetByIdRequestDto {
				Id = Id,
				Region = region ?? CountryCode2.Invariant.ToString()
            };
	}
}