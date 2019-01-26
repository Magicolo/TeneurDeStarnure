using static Game.Node;

namespace Game
{
	public static class Story
	{
		public static readonly Event ApproachThePyramid = new Event(
			nameof(ApproachThePyramid),
			Sequence(
				Line("You suddenly face an unexpected pyramid that seemed like it came out of nowhere!"),
				Delay(1f),
				Text("You approach prudently"), Text("....").Typewrite(3f), Break()),
			("Open the door", nameof(EnterThePyramid), Characters.All),
			("Run away", nameof(RunAwayFromThePyramid), Characters.Lau | Characters.Metal),
			("Knock on the door", nameof(KnockOnThePyramidDoor), Characters.Water | Characters.Earth));

		public static readonly Event EnterThePyramid = new Event(
			nameof(EnterThePyramid),
			Sequence(
				Line("You try to enter the pyramid, but a strange force prevents you from going in.")
			)
			.Typewrite(),
			("Look left", nameof(NoticeSuspiciousBush)),
			("Run away", nameof(RunAwayFromThePyramid)),
			("Knock on the door", nameof(KnockOnThePyramidDoor)));

		public static readonly Event RunAwayFromThePyramid = new Event(
			nameof(RunAwayFromThePyramid),
			Sequence(),
			("Go back to the pyramid", nameof(ApproachThePyramid)));

		public static readonly Event KnockOnThePyramidDoor = new Event(
			nameof(KnockOnThePyramidDoor),
			Sequence(
				Line("You knock on the door of the pyramid...."),
				Delay(2f),
				Line("But nothing happens."))
			.Typewrite(),
			("Run away", nameof(RunAwayFromThePyramid)),
			("Look left", nameof(NoticeSuspiciousBush)));

		public static readonly Event NoticeSuspiciousBush = new Event(
			nameof(NoticeSuspiciousBush),
			Sequence(
				Line("You notice a suspicious bush.")
			)
			.Typewrite(),
			("Run away", nameof(RunAwayFromThePyramid)),
			("Approach the bush", nameof(RunAwayFromThePyramid)));

		public static readonly Event ApproachTheBush = new Event(
			nameof(ApproachTheBush),
			Sequence(
				Line("You approach the bush."),
				Line("It is a pretty normal bush...."))
			.Typewrite(),
			("Go back to the pyramid", nameof(ApproachThePyramid)));
	}
}
