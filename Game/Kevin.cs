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
				Identifier = Game.Characters.Lau,
				Name = "What's left of Lau.",
				Description = "Once an ordinary Canadian.",
				Objective = "The Ice Wand whispers in my ear. I want it to stop! Please make it stop... End it even if it means ending me..."
			},
			new Character
			{
				Identifier = Game.Characters.Dad,
				Name = "Spirit of Dad.",
				Description = "Grumpy and stubborn. Loves Jeopardy.",
				Objective = "Even though you raised him just fine, Lau went nuts and froze the world including myself! KILL the ungrateful boy!"
			},
			//new Character
			//{
			//	Identifier = Game.Characters.Dog,
			//	Name = "Mino the dog",
			//	Description = "Arf! Arf! Grr....",
			//	Objective = "Hail to you."
			//},
			new Character
			{
				Identifier = Game.Characters.Mom,
				Name = "Spirit of Mom.",
				Description = "Neurotic and picky. Loves her dog Mino.",
				Objective = "Your son Lau is responsible for transforming the world to a frozen wasteland. Put him out of his misery!"
			},
			//new Character
			//{
			//	Identifier = Game.Characters.Pal,
			//	Name = "Pal",
			//	Description = "Neighbour the same age as Lau. They were friends until Pal used knowledge about Lau to gain favour with bullies at school.",
			//	Objective = "Rain."
			//},
		}).ToDictionary(character => character.Name);
		public readonly Dictionary<string, Event> Events = typeof(MainStory).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.Select(field => field.GetValue(null))
			.OfType<Event>()
			.ToDictionary(value => value.Identifier);
		public readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();
		public Event Event = MainStory.Vestibule;
		public string LastChoice = "";
		public Node LastOutcome = Node.Text("");
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
				Console.WriteLine($"Player {playerId} of character {player.Character.Identifier} hasn chosen #{choiceId}.");
				State.LastChoice = $"'{player.Character.Identifier}' has chosen '{choice.Label}'.";
				State.LastOutcome = choice.Outcome;
				choice.Effect(State);
				return "".ToSuccess();
			}

			return Failures.PlayerNotFound;
		}

		public static Result GetPlayer(string playerId) =>
			State.Players.TryGetValue(playerId, out var player) ? player.ToSuccess() : Failures.PlayerNotFound;

		public static Result GetState() => State.ToSuccess();

		public static Result GetCharacters() => State.Characters.ToSuccess();
		public static Result GetCurrentEventId(string playerId) => State.Event?.Identifier.ToSuccess() ?? Failures.CurrentEventNotFound;
		public static (Result, State, Player) GetCurrentEvent(string playerId)
		{
			if (State.Players.TryGetValue(playerId, out var player))
				return (State.Event?.ToSuccess() ?? Failures.CurrentEventNotFound, State, player);

			return (Failures.PlayerNotFound, State, new Player());
		}
		public static Result GetTestContent() => Story.ApproachThePyramid.ToSuccess();
	}
}
