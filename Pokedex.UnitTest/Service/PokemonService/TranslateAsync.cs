using Microsoft.Extensions.Logging;
using Moq;
using Pokedex.Api.Enumeration;
using Pokedex.Api.Service;
using Pokedex.Api.Service.Proxy.Funtranslations;
using Pokedex.Api.Service.Proxy.Pokemon;
using Pokedex.Api.Service.Proxy.Pokemon.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.UnitTest.Service.PokemonService
{
		public class TranslateAsync
		{
				private readonly Moq.Mock<IPokemonProxy> mockedPokemonProxy;
				private readonly Moq.Mock<IFuntranslationsProxy> mockedFuntranslationsProxy;
				private readonly Moq.Mock<ILogger<IPokemonService>> mockedLogger;
				private readonly Api.Service.PokemonService pokemonService;

				public TranslateAsync()
				{
						mockedPokemonProxy = new Moq.Mock<IPokemonProxy>();
						mockedFuntranslationsProxy = new Moq.Mock<IFuntranslationsProxy>();
						mockedLogger = new Moq.Mock<ILogger<IPokemonService>>();

						pokemonService = new Api.Service.PokemonService(mockedPokemonProxy.Object,
																																		mockedFuntranslationsProxy.Object,
																																		mockedLogger.Object
																																		);
				}

				[Fact]
				public async Task RightScenario_Habitat_is_Cave()
				{
						//Arrange
						string name = "wormadam";

						PokemonGetResponse pokemon = new PokemonGetResponse
						{
								FlavorTextEntries = new List<FlavorTextEntry>
								{
										new FlavorTextEntry
										{
												FlavorText= "some FlavorTextEntries"
										}
								},
								Habitat = new Api.Service.Proxy.Pokemon.Dto.Habitat
								{
										Id = 1,
										Name = "Cave",
								},
								Id = 1,
								IsLegendary = false,
								Name = name
						};

						TranslationResponse translatedText = new TranslationResponse
						{
								Contents = new Content
								{
										Translated = "translated"
								}
						};

						mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemon);
						mockedFuntranslationsProxy.Setup(x => x.GetYodaTranslation(pokemon.FlavorTextEntries.First().FlavorText)).ReturnsAsync(translatedText);

						//Act
						var result = await pokemonService.TranslateAsync(name);

						//Assert
						Assert.True(result.Success);
						Assert.Null(result.Errors);
						Assert.NotNull(result.Result);
						Assert.Equal(translatedText.Contents.Translated, result.Result.Description);
						Assert.Equal(pokemon.Habitat.Name, result.Result.Habitat);
						Assert.Equal(pokemon.IsLegendary, result.Result.IsLegendary);
						Assert.Equal(pokemon.Name, result.Result.Name);
						mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
						mockedFuntranslationsProxy.Verify(x => x.GetYodaTranslation(pokemon.FlavorTextEntries.First().FlavorText), Times.Once);
				}

				[Fact]
				public async Task RightScenario_Is_Legandary()
				{
						//Arrange
						string name = "wormadam";

						PokemonGetResponse pokemon = new PokemonGetResponse
						{
								FlavorTextEntries = new List<FlavorTextEntry>
								{
										new FlavorTextEntry
										{
												FlavorText= "some FlavorTextEntries"
										}
								},
								Habitat = new Api.Service.Proxy.Pokemon.Dto.Habitat
								{
										Id = 1,
										Name = "test",
								},
								Id = 1,
								IsLegendary = true,
								Name = name
						};

						TranslationResponse translatedText = new TranslationResponse
						{
								Contents = new Content
								{
										Translated = "translated"
								}
						};

						mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemon);
						mockedFuntranslationsProxy.Setup(x => x.GetYodaTranslation(pokemon.FlavorTextEntries.First().FlavorText)).ReturnsAsync(translatedText);

						//Act
						var result = await pokemonService.TranslateAsync(name);

						//Assert
						Assert.True(result.Success);
						Assert.Null(result.Errors);
						Assert.NotNull(result.Result);
						Assert.Equal(translatedText.Contents.Translated, result.Result.Description);
						Assert.Equal(pokemon.Habitat.Name, result.Result.Habitat);
						Assert.Equal(pokemon.IsLegendary, result.Result.IsLegendary);
						Assert.Equal(pokemon.Name, result.Result.Name);
						mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
						mockedFuntranslationsProxy.Verify(x => x.GetYodaTranslation(pokemon.FlavorTextEntries.First().FlavorText), Times.Once);
				}

				[Fact]
				public async Task RightScenario_Is_Not_Legandary_Is_Not_Cave()
				{
						//Arrange
						string name = "wormadam";

						PokemonGetResponse pokemon = new PokemonGetResponse
						{
								FlavorTextEntries = new List<FlavorTextEntry>
								{
										new FlavorTextEntry
										{
												FlavorText= "some FlavorTextEntries"
										}
								},
								Habitat = new Api.Service.Proxy.Pokemon.Dto.Habitat
								{
										Id = 1,
										Name = "test",
								},
								Id = 1,
								IsLegendary = false,
								Name = name
						};

						TranslationResponse translatedText = new TranslationResponse
						{
								Contents = new Content
								{
										Translated = "translated"
								}
						};

						mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemon);
						mockedFuntranslationsProxy.Setup(x => x.GetShakespeareTranslation(pokemon.FlavorTextEntries.First().FlavorText)).ReturnsAsync(translatedText);

						//Act
						var result = await pokemonService.TranslateAsync(name);

						//Assert
						Assert.True(result.Success);
						Assert.Null(result.Errors);
						Assert.NotNull(result.Result);
						Assert.Equal(translatedText.Contents.Translated, result.Result.Description);
						Assert.Equal(pokemon.Habitat.Name, result.Result.Habitat);
						Assert.Equal(pokemon.IsLegendary, result.Result.IsLegendary);
						Assert.Equal(pokemon.Name, result.Result.Name);
						mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
						mockedFuntranslationsProxy.Verify(x => x.GetShakespeareTranslation(pokemon.FlavorTextEntries.First().FlavorText), Times.Once);
				}

				[Fact]
				public async Task RightScenario_Yolda_Throw_Exception()
				{
						//Arrange
						string name = "wormadam";

						PokemonGetResponse pokemon = new PokemonGetResponse
						{
								FlavorTextEntries = new List<FlavorTextEntry>
								{
										new FlavorTextEntry
										{
												FlavorText= "some FlavorTextEntries"
										}
								},
								Habitat = new Api.Service.Proxy.Pokemon.Dto.Habitat
								{
										Id = 1,
										Name = "test",
								},
								Id = 1,
								IsLegendary = true,
								Name = name
						};

						TranslationResponse translatedText = new TranslationResponse
						{
								Contents = new Content
								{
										Translated = "translated"
								}
						};

						mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemon);
						mockedFuntranslationsProxy.Setup(x => x.GetYodaTranslation(pokemon.FlavorTextEntries.First().FlavorText)).Throws<Exception>();

						//Act
						var result = await pokemonService.TranslateAsync(name);

						//Assert
						Assert.True(result.Success);
						Assert.Null(result.Errors);
						Assert.NotNull(result.Result);
						Assert.Equal(pokemon.FlavorTextEntries.First().FlavorText, result.Result.Description);
						Assert.Equal(pokemon.Habitat.Name, result.Result.Habitat);
						Assert.Equal(pokemon.IsLegendary, result.Result.IsLegendary);
						Assert.Equal(pokemon.Name, result.Result.Name);
						mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
						mockedFuntranslationsProxy.Verify(x => x.GetYodaTranslation(pokemon.FlavorTextEntries.First().FlavorText), Times.Once);
				}

				[Fact]
				public async Task RightScenario_Shakespeare_Throw_Exception()
				{
						//Arrange
						string name = "wormadam";

						PokemonGetResponse pokemon = new PokemonGetResponse
						{
								FlavorTextEntries = new List<FlavorTextEntry>
								{
										new FlavorTextEntry
										{
												FlavorText= "some FlavorTextEntries"
										}
								},
								Habitat = new Api.Service.Proxy.Pokemon.Dto.Habitat
								{
										Id = 1,
										Name = "test",
								},
								Id = 1,
								IsLegendary = false,
								Name = name
						};

						TranslationResponse translatedText = new TranslationResponse
						{
								Contents = new Content
								{
										Translated = "translated"
								}
						};

						mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(pokemon);
						mockedFuntranslationsProxy.Setup(x => x.GetShakespeareTranslation(pokemon.FlavorTextEntries.First().FlavorText)).Throws<Exception>();

						//Act
						var result = await pokemonService.TranslateAsync(name);

						//Assert
						Assert.True(result.Success);
						Assert.Null(result.Errors);
						Assert.NotNull(result.Result);
						Assert.Equal(pokemon.FlavorTextEntries.First().FlavorText, result.Result.Description);
						Assert.Equal(pokemon.Habitat.Name, result.Result.Habitat);
						Assert.Equal(pokemon.IsLegendary, result.Result.IsLegendary);
						Assert.Equal(pokemon.Name, result.Result.Name);
						mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
						mockedFuntranslationsProxy.Verify(x => x.GetShakespeareTranslation(pokemon.FlavorTextEntries.First().FlavorText), Times.Once);
				}

				[Fact]
				public async Task FailedScenario_Name_Is_Empty()
				{
						//Arrange
						string name = "";

						PokemonGetResponse response = new PokemonGetResponse
						{
								FlavorTextEntries = new List<FlavorTextEntry>
								{
										new FlavorTextEntry
										{
												FlavorText= "some FlavorTextEntries"
										}
								},
								Habitat = new Api.Service.Proxy.Pokemon.Dto.Habitat
								{
										Id = 1,
										Name = "some Habitat",
								},
								Id = 1,
								IsLegendary = true,
								Name = name
						};

						//Act
						var result = await pokemonService.TranslateAsync(name);

						//Assert
						Assert.False(result.Success);
						Assert.NotNull(result.Errors);
						Assert.Null(result.Result);
						Assert.Single(result.Errors);
						Assert.Equal(ErrorType.InvalidName, result.Errors.Single().Type);
						mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Never);
						mockedFuntranslationsProxy.Verify(x => x.GetShakespeareTranslation(name), Times.Never);
						mockedFuntranslationsProxy.Verify(x => x.GetYodaTranslation(name), Times.Never);
				}
		}
}
