using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokedex.Api.Service.Proxy.Funtranslations;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Api.Service.Proxy.Shakespeare
{
		public class FuntranslationsProxy: IFuntranslationsProxy
		{
				private readonly IHttpClientFactory httpClientFactory;
				private readonly string baseUrl;

				public FuntranslationsProxy(IHttpClientFactory httpClientFactory, IConfiguration configuration)
				{
						this.httpClientFactory = httpClientFactory;
						baseUrl = configuration.GetSection("Proxy").GetSection("FuntranslationsBaseUrl").Value;
				}

				public async Task<TranslationResponse> GetShakespeareTranslation(string description)
				{
						var httpClient = httpClientFactory.CreateClient();
						var httpResponse = await httpClient.GetAsync($"{baseUrl}translate/shakespeare.json?text={description}");
						var json = await httpResponse.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<TranslationResponse>(json);
				}

				public async Task<TranslationResponse> GetYodaTranslation(string description)
				{
						var httpClient = httpClientFactory.CreateClient();
						var httpResponse = await httpClient.GetAsync($"{baseUrl}translate/yoda.json?text={description}");
						var json = await httpResponse.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<TranslationResponse>(json);
				}
		}
}
