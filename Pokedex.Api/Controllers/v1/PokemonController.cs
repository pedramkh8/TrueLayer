using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Service;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers.v1
{
		[Route("api/v1/[controller]")]
		[ApiController]
		public class PokemonController : ControllerBase
		{
				private readonly IPokemonService pokemonService;

				public PokemonController(IPokemonService pokemonService)
				{
						this.pokemonService = pokemonService;
				}

				[HttpGet("{name}")]
				public async Task<IActionResult> Get(string name)
				{
						return Ok( await pokemonService.GetPokemonAsync(name));
				}

				[HttpGet("translated/{name}")]
				public async Task<IActionResult> GetFun(string name)
				{
						return Ok(await pokemonService.Translate(name));
				}
		}
}
