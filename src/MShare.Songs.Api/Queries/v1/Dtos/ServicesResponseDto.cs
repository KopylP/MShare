using System;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Api.V1.Queries.Dtos
{
	public class ServicesResponseDto
	{
		public ItemDto[] Items { get; set; }

		public class ItemDto
		{
			public StreamingServiceType Type { get; set; }
			public string Name { get; set; }
			public bool IsAvailable { get; set; }
        }

		public static ServicesResponseDto Of(ItemDto[] items)
			=> new ServicesResponseDto
			{
				Items = items ?? Array.Empty<ItemDto>()
			};
	}
}

