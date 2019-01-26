using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public static class Kevin
	{
		static readonly Dictionary<string, Characters> _identifierToPlayer = new Dictionary<string, Characters>();
		static readonly Dictionary<Characters, string> _descriptions = new Dictionary<Characters, string>
		{
			{ Characters.Earth, "" },
			{ Characters.Fire, "" },
			{ Characters.Lau, "" },
			{ Characters.Metal, "" },
			{ Characters.Water, "" },
			{ Characters.Wood, "" }
		};


		static Event _currentEvent;

		public static (string character, string description)[] GetCharacterChoices() =>
			_descriptions.Select(pair => (pair.Key.ToString(), pair.Value)).ToArray();

		public static string GetNewPlayerId(string character)
		{
			if (Enum.TryParse<Characters>(character, out var casted))
			{
				var identifier = (_identifierToPlayer.Count + 1).ToString();
				_identifierToPlayer[identifier] = casted;
				return identifier;
			}

			return "INVALID CHARACTER";
		}

		public static string GetCharacter(string playerId) =>
			_identifierToPlayer.TryGetValue(playerId, out var character) ? character.ToString() : "CHARACTER NOT FOUND";

		public static string GetCurrentEventId(string playerId) => _currentEvent?.Identifier ?? "EVENT NOT FOUND";

		public static string GetCurrentEvent(string playerId) => _currentEvent?.Serialize() ?? "EVENT NOT FOUND";

		public static string GetTestContent() => Story.ApproachThePyramid.Serialize();

		public static string HandleAnswer(string id, string answerId) => "OK";
	}
}
