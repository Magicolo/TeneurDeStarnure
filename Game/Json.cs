﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game
{
	public static class Json
	{
		public static JObject ToJson(this Event @event) => JObject.FromObject(new
		{
			@event.Identifier,
			Script = @event.Script.Build(@event.Script),
			@event.Choices
		});

		public static JObject ToJson(this Result @object) => JObject.FromObject(@object);
		public static JToken ToJson<T>(this T value) => JToken.FromObject(value);
		public static string Serialize(this object @object) => JsonConvert.SerializeObject(@object);
	}
}