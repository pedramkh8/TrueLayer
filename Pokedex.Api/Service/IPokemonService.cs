using Pokedex.Api.Dto;
using System.Threading.Tasks;

namespace Pokedex.Api.Service
{
		internal interface IPokemonService
		{
				Task<GetResponse> GetPokemonAsync(string name);
				Task<GetResponse> Translate(string name);
		}
}
