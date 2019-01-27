using static Game.Node;

namespace Game
{
	public partial class State
	{
	}

	public static partial class MainStory
	{
		public static readonly Event ColdEnding = new Event(nameof(ColdEnding),
			Line("In the end home was just as cold as it had always been. But at least now, the rest of the world suffers along side you.")
		);

		public static readonly Event HotEnding = new Event(nameof(HotEnding),
			Sequence(
				Line("As the flames surround you, the whispers hiss and plead for mercy. The wand shoots cold blasts but the fire has taken hold of the room."),
				Break(),
				Line("Finally, the whispers stop. Thanks to Mom's habits and Dad's stubborn insistence, you will rejoin them."),
				Line("You can only hope someone out there still has someone who fills them with warmth."),
				Line("You hear yourself scream as your skin chars and you fall to the floor.")
			)
		);
	}
}
