﻿using System.Collections.Generic;
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
				Description = "A greedy curmudgeon. Prone to domestic violence.",
				Objective = "Find more potatoes."
			},
			new Character
			{
				Identifier = Game.Characters.Dog,
				Name = "Mino the dog",
				Description = "Arf! Arf! Grr....",
				Objective = "Hail to you."
			},
			new Character
			{
				Identifier = Game.Characters.Lau,
				Name = "Lau",
				Description = "The hero of our story. Left his home town to avoid his parents and found the Tao.",
				Objective = "Make a friend."
			},
			new Character
			{
				Identifier = Game.Characters.Mom,
				Name = "Mom",
				Description = "A neurotic and particular woman. Loves her dog.",
				Objective = "Find more potatoes."
			},
			new Character
			{
				Identifier = Game.Characters.Pal,
				Name = "Pal",
				Description = "Neighbour the same age as Lau. They were friends until Pal used knowledge about Lau to gain favour with bullies at school.",
				Objective = "Rain."
			},
		}).ToDictionary(character => character.Identifier.ToString());
		public readonly Dictionary<string, Event> Events = typeof(DoggoEpisode).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.Select(field => field.GetValue(null))
			.OfType<Event>()
			.ToDictionary(value => value.Identifier);
		public readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();
		public Event Event = DoggoEpisode.Vestibule;
		public string LastChoice;
	}

	public static class Kevin
	{
		public static readonly State State = new State();

		public static Result GetNewPlayerId(string characterId)
		{
			if (State.Characters.TryGetValue(characterId, out var character))
			{
				var identifier = (State.Players.Count + 1).ToString();
				var player = State.Players[identifier] = new Player
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
			if (State.Event == null) return Failures.CurrentEventNotFound;

			var choice = State.Event.Choices.FirstOrDefault(current => current.Identifier == choiceId);
			if (choice == null) return Failures.InvalidChoice;

			if (State.Players.TryGetValue(playerId, out var player))
			{
				choice.Effect(State);
				State.LastChoice = $"{player.Character.Name} has chosen {choice.Label}.";
				return State.LastChoice.ToSuccess();
			}

			return Failures.PlayerNotFound;
		}

		public static Result GetPlayer(string playerId) =>
			State.Players.TryGetValue(playerId, out var player) ? player.ToSuccess() : Failures.PlayerNotFound;

		public static Result GetState() => State.ToSuccess();

		public static Result GetCharacters() => State.Characters.ToSuccess();
		public static Result GetCurrentEventId(string playerId) => State.Event?.Identifier.ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetCurrentEvent(string playerId) => State.Event?.ToSuccess() ?? Failures.CurrentEventNotFound;
		public static Result GetTestContent() => Story.ApproachThePyramid.ToSuccess();
	}
}
