using System;
using System.Collections.Generic;

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

		public static Result GetNewPlayerId(string character)
		{
			if (Enum.TryParse<Characters>(character, out var casted))
			{
				var identifier = (_identifierToPlayer.Count + 1).ToString();
				_identifierToPlayer[identifier] = casted;
				return identifier.ToSuccess();
			}

			return "INVALID CHARACTER".ToFailure();
		}

		public static Result GetCharacter(string playerId) =>
			_identifierToPlayer.TryGetValue(playerId, out var character) ?
			character.ToString().ToSuccess() : "CHARACTER NOT FOUND".ToFailure();

		public static Result GetCharacterChoices() => _descriptions.Serialize().ToSuccess();
		public static Result GetCurrentEventId(string playerId) => _currentEvent?.Identifier.ToSuccess() ?? "EVENT NOT FOUND".ToFailure();
		public static Result GetCurrentEvent(string playerId) => _currentEvent?.Serialize().ToSuccess() ?? "EVENT NOT FOUND".ToFailure();
		public static Result GetTestContent() => Story.ApproachThePyramid.Serialize().ToSuccess();

		public static Result HandleAnswer(string id, string answerId) => default;
	}
}
