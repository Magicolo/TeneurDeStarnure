using System;

namespace Game
{
	[Flags]
	public enum Characters
	{
		All = ~0,
		Lau = 1 << 0,
		Mom = 1 << 1,
		Dad = 1 << 2,
		Dog = 1 << 3,
		Pal = 1 << 4
	}

	public static class Condition
	{
		public static Func<State, Player, bool> IsCharacter(Characters character) => (_, player) => (player.Character.Identifier & character) != 0;
	}

	public static class Effect
	{
		public static Action<State> GoTo(string eventId) => state => { if (state.Events.TryGetValue(eventId, out var @event)) state.Event = @event; };
	}

	public sealed class Choice
	{
		public static implicit operator Choice((string label, Action<State> effect) choice) =>
			new Choice(choice.label, condition: null, effect: choice.effect);
		public static implicit operator Choice((string label, Func<State, Player, bool> condition, Action<State> effect) choice) =>
			new Choice(choice.label, choice.condition, choice.effect);

		public readonly string Identifier;
		public readonly string Label;
		public readonly Node Outcome = Node.Text("");
		public readonly Func<State, Player, bool> Condition;
		public readonly Action<State> Effect;

		public Choice(string label, Func<State, Player, bool> condition = null, Action<State> effect = null) :
			this(label.Replace(' ', '_').Replace('\n', '_').Replace('\r', '_'), label, condition: condition, effect: effect)
		{ }
		public Choice(string label, Node outcome = null, Func<State, Player, bool> condition = null, Action<State> effect = null) :
			this(label.Replace(' ', '_').Replace('\n', '_').Replace('\r', '_'), label, outcome, condition, effect)
		{ }

		public Choice(string identifier, string label, Node outcome = null, Func<State, Player, bool> condition = null, Action<State> effect = null)
		{
			Identifier = identifier;
			Label = label;
			Outcome = outcome ?? Node.Text("");
			Condition = condition ?? ((_, __) => true);
			Effect = effect ?? (_ => { });
		}
	}

	public sealed class Player
	{
		public string Identifier = "";
		public Character Character;
		public string[] Notes = { };
	}

	public sealed class Character
	{
		public Characters Identifier;
		public string Name;
		public string Description;
		public string Objective;
	}

	public sealed class Event
	{
		public readonly string Identifier;
		public readonly Node Script;
		public readonly Choice[] Choices;

		public Event(string identifier, Node script, params Choice[] choices)
		{
			Identifier = identifier;
			Script = script;
			Choices = choices;
		}
	}
}
