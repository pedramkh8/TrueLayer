using Pokedex.Api.Dto;
using System.Threading.Tasks;

namespace Pokedex.Api.Service
{
    public interface IPokemonService
    {
        Task<ServiceResult<PokemonResponse>> GetPokemonAsync(string name);
        Task<ServiceResult<PokemonResponse>> TranslateAsync(string name);
    }
}
