namespace Game
{
	public static class MainStory
	{
		public static readonly Event Vestibule = new Event(
			nameof(Vestibule),
			Node.Line("The coat place. Useless now, of course. No coat is warm enough for this weather."),
			new Choice(
				"Hang self on coat hanger.",
				Node.Line("After an elaborate."),
				Condition.IsCharacter(Characters.Lau)));
	}
}
