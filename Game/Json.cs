using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Game
{
	public static class Json
	{
		sealed class EventConverter : JsonConverter<Event>
		{
			public override bool CanRead => false;
			public override bool CanWrite => true;

			readonly State _state;

			public override Event ReadJson(JsonReader reader, Type objectType, Event existingValue, bool hasExistingValue, JsonSerializer serializer) =>
				throw new NotImplementedException();

			public override void WriteJson(JsonWriter writer, Event value, JsonSerializer serializer) => JObject.FromObject(new
			{
				value.Identifier,
				Script = value.Script.Build(value.Script.Typewrite(), _state),
				value.Choices,
				Kevin.State.LastChoice,
				LastOutcome = Kevin.State.LastOutcome.Build(Kevin.State.LastOutcome.Typewrite(), _state)
			}).WriteTo(writer);

			public EventConverter(State state) { _state = state; }
		}

		public static string Serialize<T>(this T value, State state) => JsonConvert.SerializeObject(value, new EventConverter(state));
	}
}
