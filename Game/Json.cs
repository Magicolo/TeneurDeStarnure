using Newtonsoft.Json;

namespace Game
{
	public static class Json
	{
		public static string Serialize(this Event @event) => JsonConvert.SerializeObject(new
		{
			@event.Identifier,
			Script = @event.Script.Build(@event.Script),
			@event.Choices
		});
	}
}
