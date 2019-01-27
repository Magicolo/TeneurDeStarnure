using static Game.Node;

namespace Game
{
	public partial class State
	{
		public int MomKitchen;
		public int DadKitchen;
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
			new Choice("Go outside.",
				Sequence(
					Line("The wand draws you outside... Finally the whispers overcome you."),
					Line("...").Typewrite(5),
					Line("All is cold...").Typewrite(10),
					Line("All is silent...").Typewrite(7),
					Line("All is lost.").Typewrite(4)
				),
				(state, player) => player.Character.Identifier == Characters.Lau && state.MomKitchen >= 2 && state.DadKitchen >= 2,
				Effect.GoTo(nameof(ColdEnding))
			),

			new Choice("Turn on the stove.",
				Sequence(
					Line("It's a gas stove, so it hopefully still works."),
					Line("...").Typewrite(5),
					Line("The whispers are angry.")
				),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomKitchen == 0,
				state => state.MomKitchen++
			),
			new Choice("Put towel on stove.",
				Sequence(
					Line("The whispers."),
					Line("...").Typewrite(5),
					Line("THE WHISPERS!").Bold()
				),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomKitchen == 1 && state.DadKitchen == 1,
				state => state.MomKitchen++
			),
			new Choice("Go outside.",
				Sequence(
					Line("The wand draws you outside... Finally the whispers overcome you."),
					Line("...").Typewrite(5),
					Line("All is cold...").Typewrite(10),
					Line("All is silent...").Typewrite(7),
					Line("All is lost.").Typewrite(4)
				),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomKitchen >= 2 && state.DadKitchen >= 2,
				Effect.GoTo(nameof(ColdEnding))
			),
			new Choice("Go to the living room.",
				Line("You feel compelled to go to the living room and go."),
				(state, player) => player.Character.Identifier == Characters.Mom && state.DadKitchen < 2,
				Effect.GoTo(nameof(LivingRoom))
			),

			new Choice("Grab towel.",
				Line("It's soft."),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadKitchen == 0,
				state => state.DadKitchen++
			),
			new Choice("PUT TOWEL ON THE STOVE.",
				Sequence(
					Line("The towel seems to leap out of hand onto the stove."),
					Line(".....").Typewrite(5),
					Line("The towel starts burning."),
					Line(".....").Typewrite(5),
					Line("It spreads. The whispers are manic.")
				),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomKitchen == 2 && state.DadKitchen == 1,
				state => state.DadKitchen++
			),
			new Choice("Stay.",
				Sequence(
					Line("The whispers scream to go outside, but you call on Dad's stubborn attitude and watch as the flames engulf the room."),
					Line("Finally it can end...").Typewrite(8)
				),
				(state, player) => player.Character.Identifier == Characters.Dad && state.MomKitchen == 2 && state.DadKitchen == 2,
				Effect.GoTo(nameof(HotEnding))
			),
			new Choice("Go to the study.",
				Line("You feel compelled to go to the study and go."),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadKitchen < 2,
				Effect.GoTo(nameof(Study))
			)
		);
	}
}
