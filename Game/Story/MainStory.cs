using static Game.Node;

namespace Game
{
	public partial class State
	{
		public bool HasBoots = true;
		public int LauLivingRoom;
		public int MomLivingRoom;
		public int DadLivingRoom;
	}

	public static class MainStory
	{
		public static readonly Event Vestibule = new Event(nameof(Vestibule),
			Line("The coat place. Useless now, of course. No coat is warm enough for this weather."),
			new Choice("Hang self on coat hanger.",
				Line("After an elaborate. set-up, as you start to lose your breath, the hanger freezes and breaks. Now your neck is numb."),
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

		public static readonly Event LivingRoom = new Event(nameof(LivingRoom),
			Line("Mother's favorite room except for the half hour when Dad would watch Jeopardy."),

			new Choice("Go to window.",
				Line("That's a nice window."),
				(state, player) => player.Character.Identifier == Characters.Lau && state.LauLivingRoom == 0,
				state => state.LauLivingRoom++
			),
			new Choice("Smash window.",
				Sequence(
					Line("Before your hand reaches the window it is covered in a block of ice."),
					Line("The glass never stood a chance and shatters on the ground.")
				),
				(state, player) => player.Character.Identifier == Characters.Lau && state.LauLivingRoom == 1,
				state => state.LauLivingRoom++
			),
			new Choice("Cut arteries using shattered window.",
				Sequence(
					Line("Every time you draw blood, it freezes. You lose feeling in your hand, but you can still use it."),
					Text("'I'm not finished with you...' ").Italic(), Line("It whispers...")
				),
				(state, player) => player.Character.Identifier == Characters.Lau && state.LauLivingRoom == 2,
				null
			),
			new Choice("Pet the dog.",
				Line("It never let you do this before. Huh..."),
				Condition.IsCharacter(Characters.Lau),
				null
			),

			new Choice("Sit on the couch.",
				Line("There's still a groove."),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomLivingRoom == 0,
				state => { state.MomLivingRoom++; state.DadLivingRoom = 0; }
			),
			new Choice("Take all the pills on coffee table.",
				Line("You put the pills in your mouth but it's so cold in there that they just stick to your tongue and cheeks. You mouth is full of pills now."),
				(state, player) => player.Character.Identifier == Characters.Mom && state.MomLivingRoom == 1,
				null
			),
			new Choice("Tell Lau to shut up.",
				Line("You scream to yourself to shut the hell up, but you're not there to answer."),
				Condition.IsCharacter(Characters.Mom),
				null
			),

			new Choice("Sit on my chair.",
				Line("You sit on Dad's chair. For the first time."),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadLivingRoom == 0,
				state => { state.DadLivingRoom++; state.MomLivingRoom = 0; }
			),
			new Choice("Watch Jeopardy.",
				Line("You turn on the television... 'This ancient evil spirit caused a global temperature change of -60 degrees Celsius.'"),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadLivingRoom == 1,
				null
			),
			new Choice("Answer: 'Who is the Ice Wand?'",
				Line("The animator Alex Trebek remain silent."),
				(state, player) => player.Character.Identifier == Characters.Dad && state.DadLivingRoom == 2,
				null
			),
			new Choice("Go to the study.",
				Line("You feel compelled to go to the study and go."),
				Condition.IsCharacter(Characters.Dad),
				Effect.GoTo(nameof(Study)) + (state => state.DadLivingRoom = 0)
			)
		);

		public static readonly Event Study = new Event(nameof(Study),
			Line("Poulah")
		);
	}
}
