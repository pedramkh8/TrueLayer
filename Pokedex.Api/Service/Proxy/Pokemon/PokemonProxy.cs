using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokedex.Api.Service.Proxy.Pokemon.Dto;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Api.Service.Proxy.Pokemon
{
		internal class PokemonProxy : IPokemonProxy
		{
				private readonly IHttpClientFactory httpClientFactory;
				private readonly string baseUrl;

				public PokemonProxy(IHttpClientFactory httpClientFactory, IConfiguration configuration)
				{
						this.httpClientFactory = httpClientFactory;
						baseUrl = configuration.GetSection("ThirdParties").GetSection("PokemonBaseUrl").Value;
				}

				public async Task<PokemonGetResponse> GetAsync(string name)
				{
						var httpClient = httpClientFactory.CreateClient();
						var httpResponse = await httpClient.GetAsync($"{baseUrl}v2/pokemon-species/{name}");
					
						if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
						{
								return null;
						}

						var json = await httpResponse.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<PokemonGetResponse>(json);
				}

		}
}
