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

	public sealed partial class State
	{
		public readonly Dictionary<string, Character> Characters = (new[]
		{
			new Character
			{
				Identifier = Game.Characters.Dad,
				Name = "Dad",
				Description = "Eat vegetables and carrots also.",
				Objective = "Find more potatoes."
			},
			new Character
			{
				Identifier = Game.Characters.Dog,
				Name = "Dog",
				Description = "All you need is wood and particle systems.",
				Objective = "Hail to you."
			},
			new Character
			{
				Identifier = Game.Characters.Lau,
				Name = "Lau",
				Description = "He-Lau to you.",
				Objective = "Make a friend."
			},
			new Character
			{
				Identifier = Game.Characters.Mom,
				Name = "Mom",
				Description = "Clunk clunk clunk.",
				Objective = "Find more potatoes."
			},
			new Character
			{
				Identifier = Game.Characters.Pal,
				Name = "Pal",
				Description = "Falls from the sky, drinks your soup.",
				Objective = "Rain."
			},
			new Character
			{
				Identifier = Game.Characters.Sis,
				Name = "Sis",
				Description = "No fire here please.",
				Objective = "... there is no objective... or is there..."
			}
		}).ToDictionary(character => character.Identifier.ToString());
		public readonly Dictionary<string, Event> Events = typeof(Story).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.Select(field => field.GetValue(null))
			.OfType<Event>()
			.ToDictionary(value => value.Identifier);
		public readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();
		public Event Event = Story.ApproachThePyramid;
	}

	public static class Kevin
	{
		static readonly State _state = new State();

		public static Result GetNewPlayerId(string characterId)
		{
			if (_state.Characters.TryGetValue(characterId, out var character))
			{
				var identifier = (_state.Players.Count + 1).ToString();
				var player = _state.Players[identifier] = new Player
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
			if (_state.Event == null) return Failures.CurrentEventNotFound;

			var choice = _state.Event.Choices.FirstOrDefault(current => current.Identifier == choiceId);
			if (choice == null) return Failures.InvalidChoice;

			choice.Effect(_state);
			return "".ToSuccess();
		}

		public static Result GetPlayer(string playerId) =>
			_state.Players.TryGetValue(playerId, out var player) ? player.ToSuccess() : Failures.PlayerNotFound;

		public static Result GetState() => _state.ToSuccess();

		public static Result GetCharacters() => _state.Characters.ToSuccess();
		public static Result GetCurrentEventId(string playerId) => _state.Event?.Identifier.ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetCurrentEvent(string playerId) => _state.Event?.ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetTestContent() => Story.ApproachThePyramid.ToSuccess();
	}
}
