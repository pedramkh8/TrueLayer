using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers.v1
{
		[Route("api/v1/[controller]")]
		[ApiController]
		public class PokemonController : ControllerBase
		{
				[HttpGet("{name}")]
				public async Task<IActionResult> Get(string name)
				{
						return Ok("hello");
				}

				[HttpGet("translated/{name}")]
				public async Task<IActionResult> GetFun(string name)
				{
						return Ok("hello fun");
				}
		}
}
