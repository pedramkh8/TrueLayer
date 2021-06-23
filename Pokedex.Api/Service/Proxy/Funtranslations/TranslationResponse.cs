namespace Pokedex.Api.Service.Proxy.Funtranslations
{
		internal class TranslationResponse
		{
				public Success Success { get; set; }
				public Content Contents { get; set; }
		}

		internal class Success
		{
				public int Total { get; set; }
		}

		internal class Content
		{
				public string Translated { get; set; }
				public string Text { get; set; }
				public string Translation { get; set; }
		}
}
