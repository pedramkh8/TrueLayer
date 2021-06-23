using System.Net.Http;

namespace Pokedex.Api.Service.Proxy.Yoda
{
		public class YodaProxy
		{
				private readonly IHttpClientFactory httpClient;

				public YodaProxy(IHttpClientFactory httpClient)
				{
						this.httpClient = httpClient;
				}
		}
}
