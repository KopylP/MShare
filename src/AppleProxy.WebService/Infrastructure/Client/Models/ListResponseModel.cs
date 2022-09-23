using System;
namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class ListResponseModel<T>
	{
        public T[] Results { get; set; }

        public bool IsEmpty => !Results?.Any() ?? true;
    }
}

