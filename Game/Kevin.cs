using System;
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
		public enum States
		{
			None,
			VotingAll,
			VotingArbitrage,
			VotingResults
		}

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
		static readonly Dictionary<string, Event> _events = typeof(Story).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.Select(field => field.GetValue(null))
			.OfType<Event>()
			.ToDictionary(value => value.Identifier);

		static States _state;
		static Event _currentEvent = Story.ApproachThePyramid;

		public static Result GetNewPlayerId(string character)
		{
			if (Enum.TryParse<Characters>(character, out var casted))
			{
				var identifier = (_identifierToPlayer.Count + 1).ToString();
				_identifierToPlayer[identifier] = casted;
				return identifier.ToSuccess();
			}

			return Failures.InvalidCharacter;
		}

		public static Result ChooseEventChoice(string playerId, string choiceId)
		{
			if (_currentEvent == null) return Failures.CurrentEventNotFound;

			var choice = _currentEvent.Choices.FirstOrDefault(current => current.Identifier == choiceId);
			if (choice == null) return Failures.InvalidChoice;

			if (_events.TryGetValue(choice.Link, out var next))
			{
				_currentEvent = next;
				return "".ToSuccess();
			}

			return Failures.NextEventNotFound;
		}

		public static Result GetCharacter(string playerId) =>
			_identifierToPlayer.TryGetValue(playerId, out var character) ?
			character.ToString().ToSuccess() : Failures.PlayerNotFound;

		public static Result GetCharacterChoices() => _descriptions.ToJson().ToSuccess();
		public static Result GetCurrentEventId(string playerId) => _currentEvent?.Identifier.ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetCurrentEvent(string playerId) => _currentEvent?.ToJson().ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetTestContent() => Story.ApproachThePyramid.ToJson().ToSuccess();

		public static Result HandleAnswer(string id, string answerId) => default;
	}
}
