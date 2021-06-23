using System.Threading.Tasks;

namespace Pokedex.Api.Service.Proxy.Funtranslations
{
		internal interface IFuntranslationsProxy
		{
				Task<TranslationResponse> GetShakespeareTranslation(string description);

				Task<TranslationResponse> GetYodaTranslation(string description);
		}
}
