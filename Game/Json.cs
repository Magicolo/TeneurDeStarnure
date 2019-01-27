using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Game
{
	public static class Json
	{
		sealed class EventConverter : JsonConverter<Event>
		{
			public override Event ReadJson(JsonReader reader, Type objectType, Event existingValue, bool hasExistingValue, JsonSerializer serializer) =>
				throw new NotImplementedException();

			public override void WriteJson(JsonWriter writer, Event value, JsonSerializer serializer) => JObject.FromObject(new
			{
				value.Identifier,
				Script = value.Script.Build(value.Script),
				value.Choices
			}).WriteTo(writer);

			public override bool CanRead => false;
			public override bool CanWrite => true;
		}

		public static string Serialize<T>(this T value) => JsonConvert.SerializeObject(value, new EventConverter());
	}
}
