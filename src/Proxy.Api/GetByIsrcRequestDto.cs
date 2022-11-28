using System;
using System.ComponentModel.DataAnnotations;

namespace Proxy.Api
{
	public class GetByIsrcRequestDto
	{
        public string Isrc { get; set; }

        public string Region { get; set; }
    }
}

