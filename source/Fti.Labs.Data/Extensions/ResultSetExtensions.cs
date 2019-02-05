namespace Fti.Labs.Data.Extensions
{
	using System;
	using System.Text;
	using kCura.Relativity.Client;
	using kCura.Relativity.Client.DTOs;

	public static class ResultSetExtensions
	{
		public static void ThrowIfUnsuccessful(this ResultSet results, string initialMessage)
		{
			if (results.Success)
			{
				return;
			}

			var sb = new StringBuilder();
			sb.AppendLine(initialMessage ?? string.Empty);
			sb.Append(results.Message);
			foreach (var r in results.Results)
			{
				if (!r.Success)
				{
					sb.AppendLine(r.Message);
				}
			}

			throw new Exception(sb.ToString());
		}

		public static void ThrowIfUnsuccessful(this ExecuteBatchResultSet results, string initialMessage)
		{
			if (results.Success)
			{
				return;
			}

			var sb = new StringBuilder();
			sb.AppendLine(initialMessage ?? string.Empty);
			sb.Append(results.Message);
			foreach (var r in results.ResultSets)
			{
				if (r.Success)
				{
					continue;
				}

				sb.AppendLine(r.Message);
				foreach (var sResult in r.Results)
				{
					if (!sResult.Success)
					{
						sb.AppendLine(sResult.Message);
					}
				}
			}

			throw new Exception(sb.ToString());
		}

		public static void ThrowIfUnsuccessful(this QueryResult results, string initialMessage)
		{
			if (results.Success)
			{
				return;
			}

			var sb = new StringBuilder();
			sb.AppendLine(initialMessage ?? string.Empty);
			sb.Append(results.Message);

			throw new Exception(sb.ToString());
		}

		public static void ThrowIfUnsuccessful<T>(this QueryResultSet<T> results, string initialMessage)
			where T : kCura.Relativity.Client.DTOs.Artifact
		{
			if (results.Success)
			{
				return;
			}

			var sb = new StringBuilder();
			sb.AppendLine(initialMessage ?? string.Empty);
			sb.Append(results.Message);
			foreach (var r in results.Results)
			{
				if (!r.Success)
				{
					sb.AppendLine(r.Message);
				}
			}

			throw new Exception(sb.ToString());
		}

		public static void ThrowIfUnsuccessful<T>(this ResultSet<T> results, string initialMessage)
			where T : kCura.Relativity.Client.DTOs.Artifact
		{
			if (results.Success)
			{
				return;
			}

			var sb = new StringBuilder();
			sb.AppendLine(initialMessage ?? string.Empty);
			sb.Append(results.Message);
			foreach (var r in results.Results)
			{
				if (!r.Success)
				{
					sb.AppendLine(r.Message);
				}
			}

			throw new Exception(sb.ToString());
		}
	}
}
