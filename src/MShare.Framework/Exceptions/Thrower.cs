using System;
namespace MShare.Framework.Exceptions
{
	public static class Thrower
	{
		public static void Throw<T>(string? message = default) where T: Exception
		{
			throw (Exception)Activator.CreateInstance(typeof(T), message);
		}

		public static void ThrowIf<T>(bool condition, string? message = default) where T: Exception, new()
		{
			if (condition)
				Throw<T>(message);
		}
	}
}

