using Microsoft.Extensions.DependencyInjection;
using Pokedex.Api.Service.Proxy.Funtranslations;
using Pokedex.Api.Service.Proxy.Pokemon;
using Pokedex.Api.Service.Proxy.Shakespeare;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Pokedex.UnitTest")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Pokedex.Api.Service
{
    internal static class DependencyRegistrar
    {
        internal static IServiceCollection AddPokedexService(this IServiceCollection service)
        {
            service.AddScoped<IFuntranslationsProxy, FuntranslationsProxy>();
            service.AddScoped<IPokemonProxy, PokemonProxy>();
            service.AddScoped<IPokemonService, PokemonService>();
            return service;
        }
    }
}
