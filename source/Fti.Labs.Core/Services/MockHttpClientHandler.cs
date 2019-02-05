using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fti.Labs.Core.Services
{
	public class MockHttpClientHandler : HttpClientHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var url = request.RequestUri.ToString();

			var knownUrlKey = UrlToFileDictionary.Keys.FirstOrDefault(k => url.ToLower().StartsWith(k.ToLower()));
			if (!string.IsNullOrEmpty(knownUrlKey))
			{
				var resource = UrlToFileDictionary[knownUrlKey];

				var content =
					new StringContent(Encoding.UTF8.GetString((byte[])resource), Encoding.UTF8, "application/json");

				return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
			}

			throw new Exception($"Request for unknown url: {url}");
		}

		private Dictionary<string, object> UrlToFileDictionary = new Dictionary<string, object>
		{
			["http://example.com/processdocument"] = "{ Success: true }"
		};
	}
}
