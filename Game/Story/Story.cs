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
			Sequence(),
			("Look left", ""));

		public static readonly Event RunAwayFromThePyramid;
		public static readonly Event KnockOnThePyramidDoor;
	}
}
