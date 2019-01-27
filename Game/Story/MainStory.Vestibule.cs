using static Game.Node;

namespace Game
{
	public partial class State
	{
		public bool HasBoots = true;
	}

	public static partial class MainStory
	{
		public static readonly Event Vestibule = new Event(nameof(Vestibule),
			Line("The coat place. Useless now, of course. No coat is warm enough for this weather."),
			new Choice("Hang self on coat hanger.",
				Line("After an elaborate set-up, as you start to lose your breath, the hanger freezes and breaks. Now your neck is numb."),
				Condition.IsCharacter(Characters.Lau)
			),
			new Choice("Take off your boots!",
				Line("Your mom could never tolerate keeping your boots on in the house. You take your boots off. The snow feels nice under your feet."),
				(state, player) => player.Character.Identifier == Characters.Mom && state.HasBoots,
				state => state.HasBoots = false
			),
			new Choice("Go to the living room.",
				Line("You feel compelled to go to the living room and go."),
				(state, player) => player.Character.Identifier == Characters.Mom && !state.HasBoots,
				Effect.GoTo(nameof(LivingRoom))
			),
			new Choice("Go to study.",
				Line("You feel compelled to go to the study and go."),
				Condition.IsCharacter(Characters.Dad),
				Effect.GoTo(nameof(Study))
			)
		);
	}
}
