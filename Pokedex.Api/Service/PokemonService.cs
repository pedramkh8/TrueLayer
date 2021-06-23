using Microsoft.Extensions.Logging;
using Pokedex.Api.Dto;
using Pokedex.Api.Enumeration;
using Pokedex.Api.Service.Proxy.Funtranslations;
using Pokedex.Api.Service.Proxy.Pokemon;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Service
{
		internal class PokemonService : IPokemonService
		{
				private readonly IPokemonProxy pokemonProxy;
				private readonly IFuntranslationsProxy funtranslationsProxy;
				private readonly ILogger<PokemonService> logger;

				public PokemonService(IPokemonProxy pokemonProxy,
						IFuntranslationsProxy funtranslationsProxy,
						ILogger<PokemonService> logger)
				{
						this.pokemonProxy = pokemonProxy;
						this.funtranslationsProxy = funtranslationsProxy;
						this.logger = logger;
				}

				public async Task<GetResponse> GetPokemonAsync(string name)
				{
						var result = await pokemonProxy.GetAsync(name);

						if (result != null)
						{
								return new GetResponse
								{
										Description = result.FlavorTextEntries.FirstOrDefault()?.FlavorText,
										Habitat = result.Habitat?.Name,
										IsLegendary = result.IsLegendary,
										Name = result.Name
								};
						}

						return null;
				}

				public async Task<GetResponse> Translate(string name)
				{
						var pokemon = await GetPokemonAsync(name);

						if (pokemon is null)
						{
								return null;
						}

						var result = new GetResponse
						{
								Habitat = pokemon.Habitat,
								IsLegendary = pokemon.IsLegendary,
								Name = pokemon.Name,
								Description = pokemon.Description
						};

						try
						{
								string normalDescription = RemoveNewLine(pokemon.Description);

								if (pokemon.Habitat == Habitat.cave.ToString() || pokemon.IsLegendary)
								{
										result.Description = (await funtranslationsProxy.GetYodaTranslation(normalDescription)).Contents.Translated;
								}
								else
								{
										result.Description = (await funtranslationsProxy.GetShakespeareTranslation(normalDescription)).Contents.Translated;
								}
						}
						catch (Exception ex)
						{
								logger.LogError(ex, ex.Message);
						}

						return result;
				}

				private string RemoveNewLine(string text)
				{
						return System.Text.RegularExpressions.Regex.Replace(text, @"\t|\n|\r", "");
				}
		}
}
