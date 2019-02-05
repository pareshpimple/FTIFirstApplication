namespace Fti.Labs.Core.Services
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Fti.Labs.Core.Interfaces;
	using kCura.Relativity.Client.DTOs;
	using Newtonsoft.Json;

	public class DocumentProcessingService : IDocumentProcessingService
	{
		public async Task ProcessDocument(Document document)
		{
			using (var httpClient = new HttpClient(new MockHttpClientHandler()))
			{
				var content = JsonConvert.SerializeObject(document);
				await httpClient.PostAsync("http://example.com/processdocument", new StringContent(content));
			}
		}
	}
}
