using static Game.Node;

namespace Game
{
	public partial class State
	{
	}

	public static partial class MainStory
	{
		public static readonly Event ColdEnding = new Event(nameof(ColdEnding),
			Line("")
		);

		public static readonly Event HotEnding = new Event(nameof(HotEnding),
			Line("")
		);
	}
}
