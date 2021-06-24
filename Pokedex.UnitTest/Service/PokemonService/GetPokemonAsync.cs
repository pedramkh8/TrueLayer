using Microsoft.Extensions.Logging;
using Moq;
using Pokedex.Api.Enumeration;
using Pokedex.Api.Service;
using Pokedex.Api.Service.Proxy.Funtranslations;
using Pokedex.Api.Service.Proxy.Pokemon;
using Pokedex.Api.Service.Proxy.Pokemon.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.UnitTest.Service.PokemonService
{
    public class GetPokemonAsync
    {
        private readonly Moq.Mock<IPokemonProxy> mockedPokemonProxy;
        private readonly Moq.Mock<IFuntranslationsProxy> mockedFuntranslationsProxy;
        private readonly Moq.Mock<ILogger<IPokemonService>> mockedLogger;
        private readonly Api.Service.PokemonService pokemonService;

        public GetPokemonAsync()
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
        public async Task RightScenario()
        {
            //Arrange
            string name = "wormadam";

						PokemonGetResponse response = new PokemonGetResponse
						{
								FlavorTextEntries = new List<FlavorTextEntry>
								{
										new FlavorTextEntry
										{
												FlavorText= "some FlavorTextEntries",
												Language = new Api.Service.Proxy.Pokemon.Dto.Language
												{
													Name= Api.Enumeration.Language.en.ToString()
												}
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

            mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(response);

            //Act
            var result = await pokemonService.GetPokemonAsync(name);

            //Assert
            Assert.True(result.Success);
            Assert.Null(result.Errors);
            Assert.NotNull(result.Result);
            mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
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
												FlavorText= "some FlavorTextEntries",
												Language = new Api.Service.Proxy.Pokemon.Dto.Language
												{
													Name= Api.Enumeration.Language.en.ToString()
												}
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

            mockedPokemonProxy.Setup(x => x.GetAsync(name)).ReturnsAsync(response);

            //Act
            var result = await pokemonService.GetPokemonAsync(name);

            //Assert
            Assert.False(result.Success);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Result);
            Assert.Single(result.Errors);
            Assert.Equal(ErrorType.InvalidName, result.Errors.Single().Type);
            mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Never);
        }

        [Fact]
        public async Task FailedScenario_Pokemon_Not_Found()
        {
            //Arrange
            string name = "something";
            mockedPokemonProxy.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(null as PokemonGetResponse);

            //Act
            var result = await pokemonService.GetPokemonAsync(name);

            //Assert
            Assert.False(result.Success);
            Assert.NotNull(result.Errors);
            Assert.Null(result.Result);
            Assert.Single(result.Errors);
            Assert.Equal(ErrorType.PokemonNotFound, result.Errors.Single().Type);
            mockedPokemonProxy.Verify(x => x.GetAsync(name), Times.Once);
        }
    }
}
