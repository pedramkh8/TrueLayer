using Pokedex.Api.Service.Proxy.Pokemon.Dto;
using System.Threading.Tasks;

namespace Pokedex.Api.Service.Proxy.Pokemon
{
		public interface IPokemonProxy
		{
				Task<PokemonGetResponse> GetAsync(string name);
		}
}
