using System.Net.Http;

namespace Pokedex.Api.Service.Proxy.Pokemon
{
		public class PokemonProxy
		{
				private readonly IHttpClientFactory httpClient;

				public PokemonProxy(IHttpClientFactory httpClient)
				{
						this.httpClient = httpClient;
				}

		}
}
