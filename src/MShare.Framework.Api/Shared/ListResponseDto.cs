using System;
namespace MShare.Framework.Api.Shared
{
	public record ListResponseDto<T>
	{
		public bool IsEmpty => !Items?.Any() ?? true;
		public int Count => Items?.Length ?? 0;
		public T[] Items { get; set; }

		public static ListResponseDto<T> Of(IEnumerable<T> items)
			=> new ListResponseDto<T> { Items = items?.ToArray() ?? Array.Empty<T>() };

        public static ListResponseDto<T> Of(T item)
            => new ListResponseDto<T> { Items = new T[] { item } };
    }
}