using static Game.Node;

namespace Game
{
	public partial class State
	{
		public int LauStudy;
		public int MomStudy;
		public int DadStudy;
	}

	public static partial class MainStory
	{
		public static readonly Event Study = new Event(nameof(Study),
			Line("You watch your legs go into Dad's study. It looks better with the walls all white like this."),

			new Choice("Take sword from wall.",
				Line("You hope the sword's sharp, sleek edge will allow you to pass to the next stage of existence. There will be no whispering there!"),
				(state, player) => player.Character.Identifier == Characters.Lau && state.LauStudy == 0,
				state => state.LauStudy++
			),
			new Choice("Merge sword with Ice Wand.",
				Line("No! ... The whispers grow louder. The Ice Sword is pleased."),
				(state, player) => player.Character.Identifier == Characters.Lau && state.LauStudy == 1,
				state => state.LauStudy++
			),
			new Choice("Cut the wall down.",
				Sequence(
					Line("The Ice Sword slices the wall like a cheesecake."),
					Line("...").Typewrite(5),
					Line("Is that a secret room?")
				),
				(state, player) => player.Character.Identifier == Characters.Lau && state.LauStudy == 2,
				state => state.LauStudy++
			),
			new Choice("Go in the secret room.",
				Line("You traverse the now cut down fourth wall and enter the secret room."),
				(state, player) => player.Character.Identifier == Characters.Lau && state.LauStudy == 3,
				Effect.GoTo(nameof(FourthWall))
			),
			new Choice("Embrace the cold.",
				Line("Your skin trembles as the wind caresses it."),
				Condition.IsCharacter(Characters.Lau),
				null
			),

			new Choice("Tidy up.",
				Line("You can't help but rearrange things. All these papers and weapons all over the place.... Can't that man just put things where they belong?"),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomStudy == 0,
				state => state.MomStudy++
			),
			new Choice("Sweep.",
				Line("So much snow.... You zone out while sweeping it all into one corner."),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomStudy == 1,
				null
			),
			new Choice("Go to the living room.",
				Line("You feel compelled to go to the living room and go."),
				(state, player) => player.Character.Identifier == Characters.Mom && !state.HasBoots,
				Effect.GoTo(nameof(LivingRoom))
			),
			new Choice("Go to the vestibule.",
				Line("You feel compelled to go back to the vestibule."),
				(state, player) => player.Character.Identifier == Characters.Mom && state.HasBoots,
				Effect.GoTo(nameof(Vestibule))
			),
			new Choice("Go to kitchen.",
				Line("Perhaps it is time to make dinner. You go to the kitchen."),
				(state, player) => player.Character.Identifier == Characters.Mom && !state.HasBoots && state.MomLivingRoom > 1 && state.DadStudy > 2,
				Effect.GoTo(nameof(Kitchen))
			),

			new Choice("Sit at desk.",
				Line("The feeling of unfinished business draws your butt toward the chair."),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadStudy == 0,
				state => state.DadStudy++
			),
			new Choice("Open gun drawer.",
				Line("What was the passcode again? Let's try F-R-E-E-Z-E. It's open!"),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadStudy == 1,
				state => state.DadStudy++
			),
			new Choice("Shoot self.",
				Line("The gun freezes as you watch your hand pick it up. When you pull the trigger, it explodes in shards of frozen iron. Your ears are ringing, and your hand bleeds for a moment before the blood freezes. The whispers are angry...."),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadStudy == 2,
				state => state.DadStudy++
			),
			new Choice("Business.",
				Line("Business, business, business!"),
				Condition.IsCharacter(Characters.Dad),
				null
			)
		);
	}
}
