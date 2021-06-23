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

				public PokemonService(IPokemonProxy pokemonProxy, IFuntranslationsProxy funtranslationsProxy)
				{
						this.pokemonProxy = pokemonProxy;
						this.funtranslationsProxy = funtranslationsProxy;
				}

				public async Task<GetResponse> GetPokemonAsync(string name)
				{
						var result = await pokemonProxy.GetAsync(name);

						return new GetResponse
						{
								Description = result.FlavorTextEntries.FirstOrDefault()?.FlavorText,
								Habitat = result.Habitat?.Name,
								IsLegendary = result.IsLegendary,
								Name = result.Name
						};
				}

				public async Task<GetResponse> Translate(string name)
				{
						var pokemon = await GetPokemonAsync(name);
						var result = new GetResponse
						{
								Habitat = pokemon.Habitat,
								IsLegendary = pokemon.IsLegendary,
								Name = pokemon.Name,
								Description = pokemon.Description
						};

						try
						{
								if (pokemon.Habitat == Habitat.cave.ToString() || pokemon.IsLegendary)
								{
										result.Description = (await funtranslationsProxy.GetYodaTranslation(pokemon.Description)).Contents.Translated;
								}
								else
								{
										result.Description = (await funtranslationsProxy.GetShakespeareTranslation(pokemon.Description)).Contents.Translated;
								}
						}
						catch (Exception ex)
						{
								//todo Log
						}

						return result;
				}
		}
}
