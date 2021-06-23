using System.Net.Http;

namespace Pokedex.Api.Service.Proxy.Shakespeare
{
		public class ShakespeareProxy
		{
				private readonly IHttpClientFactory httpClient;

				public ShakespeareProxy(IHttpClientFactory httpClient)
				{
						this.httpClient = httpClient;
				}
		}
}
