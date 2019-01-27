using static Game.Node;

namespace Game
{
	public partial class State
	{
	}

	public static partial class MainStory
	{
		public static readonly Event Kitchen = new Event(nameof(Kitchen),
			Line(""),

			new Choice("Go to the living room.",
				Line("You feel compelled to go to the living room and go."),
				Condition.IsCharacter(Characters.Lau),
				Effect.GoTo(nameof(LivingRoom))
			),

			new Choice("Turn on the stove.",
				Line("It's a gas stove, so it still works."),
				Condition.IsCharacter(Characters.Mom),
				Effect.GoTo(nameof(LivingRoom))
			)
		);
	}
}
