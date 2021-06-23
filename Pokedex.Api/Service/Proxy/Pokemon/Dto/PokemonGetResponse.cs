using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokedex.Api.Service.Proxy.Pokemon.Dto
{
		internal class PokemonGetResponse
		{
				[JsonProperty("id")]
				public int Id { get; set; }

				[JsonProperty("name")]
				public string Name { get; set; }

				[JsonProperty("is_legendary")]
				public bool IsLegendary { get; set; }

				[JsonProperty("flavor_text_entries")]
				public IList<FlavorTextEntry> FlavorTextEntries { get; set; }

				public Habitat Habitat { get; set; }
		}

		public class FlavorTextEntry
		{
				[JsonProperty("flavor_text")]
				public string FlavorText { get; set; }
		}

		internal class Habitat
		{
				public int Id { get; set; }
				public string Name { get; set; }
				public IList<Name> Names { get; set; }
		}

		internal class Name
		{
				public string name { get; set; }
		}
}
