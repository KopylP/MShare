using System;
namespace MShare.Framework.Exceptions
{
	public static class Thrower
	{
		public static void Throw<T>(string? message = default) where T: Exception, new()
		{
			var exception = new T();
			var propertyInfo = typeof(T).GetProperty("Message");
			propertyInfo?.SetValue(exception, message);

			throw exception;
		}

		public static void ThrowIf<T>(bool condition, string? message = default) where T: Exception, new()
		{
			if (condition)
				Throw<T>(message);
		}
	}
}

