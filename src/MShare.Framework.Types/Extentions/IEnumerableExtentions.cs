using System;
namespace MShare.Framework.Types.Extentions
{
	public static class IEnumerableExtentions
	{
		public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, SimpleSpecification<T> specification)
			=> collection.Where(specification.Clause);
    }
}

