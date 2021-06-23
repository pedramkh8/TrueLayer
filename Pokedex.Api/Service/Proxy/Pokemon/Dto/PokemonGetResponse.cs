using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.Api.Service.Proxy.Pokemon.Dto
{
		public class PokemonGetResponse
		{
				[JsonProperty("id")]
				public int Id { get; set; }

				[JsonProperty("name")]
				public string Name { get; set; }

				[JsonProperty("gender_rate")]
				public int GenderRate { get; set; }

				[JsonProperty("flavor_text_entries")]
				public IList<FlavorTextEntry> FlavorTextEntries { get; set; }
		}

		public class FlavorTextEntry
		{
				[JsonProperty("flavor_text")]
				public string FlavorText { get; set; }
		}
}
