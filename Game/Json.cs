using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Game
{
	public static class Json
	{
		sealed class EventConverter : JsonConverter<Event>
		{
			public override bool CanRead => false;
			public override bool CanWrite => true;

			readonly State _state;
			readonly Player _player;

			public override Event ReadJson(JsonReader reader, Type objectType, Event existingValue, bool hasExistingValue, JsonSerializer serializer) =>
				throw new NotImplementedException();

			public override void WriteJson(JsonWriter writer, Event value, JsonSerializer serializer) => JObject.FromObject(new
			{
				value.Identifier,
				Script = value.Script.Build(value.Script.Typewrite(), _state),
				Choices = value.Choices.Where(choice => choice.Condition(_state, _player)).ToArray(),
				Kevin.State.LastChoice,
				LastOutcome = Kevin.State.LastOutcome.Build(Kevin.State.LastOutcome.Typewrite(), _state)
			}).WriteTo(writer);

			public EventConverter(State state, Player player) { _state = state; _player = player; }
		}

		public static string Serialize<T>(this T value, State state, Player player) => JsonConvert.SerializeObject(value, new EventConverter(state, player));
		public static string Serialize<T>(this T value) => JsonConvert.SerializeObject(value);
	}
}
