using static Game.Node;

namespace Game
{
	public static class Story
	{
		public static readonly Event ApproachThePyramid = new Event(
			nameof(ApproachThePyramid),
			Sequence(
				Line("You suddenly face an unexpected pyramid that seemed like it came out of nowhere!").Color(200, 50, 50),
				Delay(1f),
				Text("You approach prudently"), Text("....").Thickness(1000).Typewrite(3f), Break())
			.Typewrite(),
			("Open the door", Effect.GoTo(nameof(EnterThePyramid))),
			("Run away", Effect.GoTo(nameof(RunAwayFromThePyramid))),
			("Knock on the door", Effect.GoTo(nameof(KnockOnThePyramidDoor))));

		public static readonly Event EnterThePyramid = new Event(
			nameof(EnterThePyramid),
			Sequence(
				Line("You try to enter the pyramid, but a strange force prevents you from going in.").Bold()
			)
			.Typewrite(),
			("Look left", Effect.GoTo(nameof(NoticeSuspiciousBush))),
			("Run away", Effect.GoTo(nameof(RunAwayFromThePyramid))),
			("Knock on the door", Effect.GoTo(nameof(KnockOnThePyramidDoor))));

		public static readonly Event RunAwayFromThePyramid = new Event(
			nameof(RunAwayFromThePyramid),
			Sequence(),
			("Go back to the pyramid", Effect.GoTo(nameof(ApproachThePyramid))));

		public static readonly Event KnockOnThePyramidDoor = new Event(
			nameof(KnockOnThePyramidDoor),
			Sequence(
				Line("You knock on the door of the pyramid....").Italic(),
				Delay(2f),
				Line("But nothing happens.").Underline())
			.Typewrite(),
			("Run away", Effect.GoTo(nameof(RunAwayFromThePyramid))),
			("Look left", Effect.GoTo(nameof(NoticeSuspiciousBush))));

		public static readonly Event NoticeSuspiciousBush = new Event(
			nameof(NoticeSuspiciousBush),
			Sequence(
				Line("You notice a suspicious bush.").LineThrough()
			)
			.Typewrite(),
			("Run away", Effect.GoTo(nameof(RunAwayFromThePyramid))),
			("Approach the bush", Effect.GoTo(nameof(RunAwayFromThePyramid))));

		public static readonly Event ApproachTheBush = new Event(
			nameof(ApproachTheBush),
			Sequence(
				Line("You approach the bush.").Oblique(),
				Line("It is a pretty normal bush...."))
			.Typewrite(),
			("Go back to the pyramid", Effect.GoTo(nameof(ApproachThePyramid))));
	}
}
