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

				public async Task<ServiceResult<PokemonResponse>> GetPokemonAsync(string name)
				{
						if (string.IsNullOrEmpty(name))
						{
								return new ServiceResult<PokemonResponse>(new ErrorResult(ErrorType.InvalidName));
						}

						var result = await pokemonProxy.GetAsync(name);

						if (result is null)
						{
								return new ServiceResult<PokemonResponse>(new ErrorResult(ErrorType.PokemonNotFound));
						}

						return new ServiceResult<PokemonResponse>(new PokemonResponse
						{
								Description = result.FlavorTextEntries.FirstOrDefault()?.FlavorText,
								Habitat = result.Habitat?.Name,
								IsLegendary = result.IsLegendary,
								Name = result.Name
						});
				}

				public async Task<ServiceResult<PokemonResponse>> TranslateAsync(string name)
				{
						var serviceResult = await GetPokemonAsync(name);

						if (!serviceResult.Success)
						{
								return new ServiceResult<PokemonResponse>(serviceResult.Errors);
						}

						var result = new PokemonResponse
						{
								Habitat = serviceResult.Result.Habitat,
								IsLegendary = serviceResult.Result.IsLegendary,
								Name = serviceResult.Result.Name,
								Description = serviceResult.Result.Description
						};

						try
						{
								string normalDescription = RemoveNewLine(serviceResult.Result.Description);

								if (serviceResult.Result.Habitat == Habitat.cave.ToString() || serviceResult.Result.IsLegendary)
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

						return new ServiceResult<PokemonResponse>(result);
				}

				private string RemoveNewLine(string text)
				{
						return System.Text.RegularExpressions.Regex.Replace(text, @"\t|\n|\r", "");
				}
		}
}
