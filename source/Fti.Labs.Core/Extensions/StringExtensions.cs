namespace Fti.Labs.Core.Extensions
{
	public static class StringExtensions
	{
		public static string Truncate(this string original, int limit = 15)
		{
			if (string.IsNullOrEmpty(original) || limit < 1)
				return original;

			return original.Length > limit
				? $"{original.Substring(0, limit - 1)}"
				: original;
		}
	}
}
