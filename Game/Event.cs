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
		Sis = 1 << 4,
		Pal = 1 << 5
	}

	public sealed class Choice
	{
		public static implicit operator Choice(in (string label, string link, Characters characters) choice) => new Choice(choice.label, choice.link, choice.characters);
		public static implicit operator Choice(in (string label, string link) choice) => new Choice(choice.label, choice.link);

		public readonly string Identifier;
		public readonly string Label;
		public readonly string Link;
		public readonly Characters Characters;

		public Choice(string label, string link, Characters characters = Characters.All) :
			this(label.Replace(' ', '_').Replace('\n', '_').Replace('\r', '_'), label, link, characters)
		{ }

		public Choice(string identifier, string label, string link, Characters characters = Characters.All)
		{
			Identifier = identifier;
			Label = label;
			Link = link;
			Characters = characters;
		}
	}

	public sealed class Event
	{
		public readonly string Identifier;
		public readonly Node Script;
		public readonly Choice[] Choices;

		public Event(string identifier, in Node script, params Choice[] choices)
		{
			Identifier = identifier;
			Script = script;
			Choices = choices;
		}
	}
}
