using System;
namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class ListResponseModel<T>
	{
        public T[] Data { get; set; }

        public bool IsEmpty => !Data?.Any() ?? true;
    }
}

