using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
	public static class Failures
	{
		public static readonly Result CurrentEventNotFound = nameof(CurrentEventNotFound).ToFailure();
		public static readonly Result NextEventNotFound = nameof(NextEventNotFound).ToFailure();
		public static readonly Result PlayerNotFound = nameof(PlayerNotFound).ToFailure();
		public static readonly Result InvalidCharacter = nameof(InvalidCharacter).ToFailure();
		public static readonly Result InvalidChoice = nameof(InvalidChoice).ToFailure();
	}

	public static class Kevin
	{
		static readonly Dictionary<string, Character> _characters = new[]
		{
			new Character
			{
				Identifier = Characters.Earth,
				Name = "Earth",
				Description = "Eat vegetables and carrots also.",
				Objective = "Find more potatoes."
			},
			new Character
			{
				Identifier = Characters.Fire,
				Name = "Fire",
				Description = "All you need is wood and particle systems.",
				Objective = "Hail to you."
			},
			new Character
			{
				Identifier = Characters.Lau,
				Name = "Lau",
				Description = "He-Lau to you.",
				Objective = "Make a friend."
			},
			new Character
			{
				Identifier = Characters.Metal,
				Name = "Metal",
				Description = "Clunk clunk clunk.",
				Objective = "Find more potatoes."
			},
			new Character
			{
				Identifier = Characters.Water,
				Name = "Water",
				Description = "Falls from the sky, drinks your soup.",
				Objective = "Rain."
			},
			new Character
			{
				Identifier = Characters.Wood,
				Name = "Wood",
				Description = "No fire here please.",
				Objective = "... there is no objective... or is there..."
			}
		}.ToDictionary(character => character.Identifier.ToString());
		static readonly Dictionary<string, Event> _events = typeof(Story).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.Select(field => field.GetValue(null))
			.OfType<Event>()
			.ToDictionary(value => value.Identifier);
		static readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
		static Event _event = Story.ApproachThePyramid;

		public static Result GetNewPlayerId(string characterId)
		{
			if (_characters.TryGetValue(characterId, out var character))
			{
				var identifier = (_players.Count + 1).ToString();
				var player = _players[identifier] = new Player
				{
					Identifier = identifier,
					Character = character,
					Notes = { }
				};
				return player.ToSuccess();
			}

			return Failures.InvalidCharacter;
		}

		public static Result ChooseEventChoice(string playerId, string choiceId)
		{
			if (_event == null) return Failures.CurrentEventNotFound;

			var choice = _event.Choices.FirstOrDefault(current => current.Identifier == choiceId);
			if (choice == null) return Failures.InvalidChoice;

			if (_events.TryGetValue(choice.Link, out var next))
			{
				_event = next;
				return "".ToSuccess();
			}

			return Failures.NextEventNotFound;
		}

		public static Result GetPlayer(string playerId) =>
			_players.TryGetValue(playerId, out var player) ? player.ToSuccess() : Failures.PlayerNotFound;

		public static Result GetCharacters() => _characters.ToSuccess();
		public static Result GetCurrentEventId(string playerId) => _event?.Identifier.ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetCurrentEvent(string playerId) => _event?.ToJson().ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetTestContent() => Story.ApproachThePyramid.ToJson().ToSuccess();
	}
}
