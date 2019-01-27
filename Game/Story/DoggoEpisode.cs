//using static Game.Node;

//namespace Game
//{
//    public partial class State
//    {
//        public bool BootsOn;
//        public int BeardLength = 1;
//        public static Event LastEvent;
//    }

//    public static class DoggoEpisode
//    {
//        //public static Choice Meditate = new Choice("Meditate", Effect.GoTo(nameof(Meditation)) + ((state) => state.LastEvent = ))
//        //public static Event Meditation = new Event(Line("...").Typewrite(), Effect.GoTo(state.LastEvent);
//        //public static bool bootsOn = true;

//        //public static string BootsEvent
//        //{
//        //    get
//        //    {
//        //        return bootsOn ? nameof(TakeOffBoots) : nameof(PutOnBoots);
//        //    }
//        //}

//        //public static bool coatOn = true;

//        //public static string CoatEvent
//        //{
//        //    get
//        //    {
//        //        return coatOn ? nameof(TakeOffCoat) : nameof(PutOnCoat);
//        //    }
//        //}

//        //public static Choice bootsOn = new Choice;
//        //public static bool doIHaveMyBootsOn = true;
//        //public static Choice BootsChoice
//        //{
//        //    get
//        //    {
//        //        return doIHaveMyBootsOn ? bootsOn : bootsOff;
//        //    }
//        //}

//        public static Event Vestibule = new Event(nameof(Vestibule),
//                Line("Welp. I'm home."),
//                    ("Hang up coat.", Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Snow in July... At least it's an interesting apocalypse.", Effect.GoTo(nameof(Vestibule))),
//                    ("Go to the living room", Effect.GoTo(nameof(LivingRoomMain)))

//        );

//        public static Event TakeOffCoat = new Event(nameof(TakeOffCoat),
//                Line("Might as well feel physically comfortable."),
//                    ("Put coat back on.", Effect.GoTo(nameof(PutOnCoat))),
//                    ("Take off boots.", Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Go to the living room", Effect.GoTo(nameof(LivingRoomMain)))

//        );

//        public static Event PutOnCoat = new Event(nameof(PutOnCoat),
//               Line("I can't belive how cold it's gotten. The end really is coming."),
//                   ("Hang up coat.", Effect.GoTo(nameof(TakeOffCoat))),
//                   ("Take off boots.", (state, _) => state.BootsOn == true, Effect.GoTo(nameof(TakeOffBoots))),
//                   ("Go to the living room", Condition.IsCharacter(Characters.Dog | Characters.Mom), Effect.GoTo(nameof(LivingRoomMain)))

//       );

//        public static Event TakeOffBoots = new Event(nameof(TakeOffBoots),
//                Line("Mom would be pretty pissed if she saw me in the house with them on."),
//                    ("Hang up coat.", Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Put boots back on.", Effect.GoTo(nameof(PutOnBoots))),
//                    ("Go to the living room", Effect.GoTo(nameof(LivingRoomMain)))

//        );

//        public static Event PutOnBoots = new Event(nameof(PutOnBoots),
//                Line("Might as well feel physically comfortable."),
//                    ("Hang up coat.", Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Take off boots.", Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Go to the living room", Effect.GoTo(nameof(LivingRoomMain)))

//        );
//        public static Event LivingRoomMain = new Event(nameof(LivingRoomMain),
//                Line("I still don't feel like I belong here. They still have the dog bed here? It's been here for what, thirty years? Oh jeeze. I'm old."),
//                    ("Climb out the window.", Condition.IsCharacter(Characters.Pal), Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Go to the vestibule.", Effect.GoTo(nameof(Vestibule))),
//                    ("Sit on the couch.", Effect.GoTo(nameof(OnTheCouch))),
//                    ("Sit on Dad's chair.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(OnDadsChair))),
//                    ("Go to the dog bed.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(DogBed)))

//        );
//        public static Event LivingRoomFromDogBed = new Event(nameof(LivingRoomFromDogBed),
//                Line("That's enough of that. There are places for humans to sit."),
//                    ("Climb out the window.", Condition.IsCharacter(Characters.Pal), Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Go to the vestibule.", Effect.GoTo(nameof(Vestibule))),
//                    ("Sit on the couch.", Effect.GoTo(nameof(OnTheCouch))),
//                    ("Sit on Dad's chair.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(OnDadsChair))),
//                    ("Go to the dog bed.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(DogBed)))

//        );
//        public static Event LivingRoomFromCouch = new Event(nameof(LivingRoomFromCouch),
//                Line("What else...?"),
//                    ("Climb out the window.", Condition.IsCharacter(Characters.Pal), Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Go to the vestibule.", Effect.GoTo(nameof(Vestibule))),
//                    ("Sit on the couch.", Effect.GoTo(nameof(OnTheCouch))),
//                    ("Sit on Dad's chair.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(OnDadsChair))),
//                    ("Go to the dog bed.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(DogBed)))

//        );
//        public static Event DogBed = new Event(nameof(DogBed),
//                Line("I still don't feel like I belong here. They still have the dog bed here? It's been here for what, thirty years? Oh jeeze. I'm old."),
//                    ("Turn away.", Effect.GoTo(nameof(LivingRoomMain))),
//                    ("Climb out the window.", Condition.IsCharacter(Characters.Pal), Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Yell at dog.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Sit on the dog bed.", Condition.IsCharacter(Characters.Mom), Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Pet dog.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(PettingTheDog)))

//        );
//        public static Event PettingTheDog = new Event(nameof(PettingTheDog),
//                Line("His fur is wiry. Not even soft at all. Mom would hold him to her face like he was a security blanket."),
//                    ("Turn away.", Effect.GoTo(nameof(LivingRoomMain))),
//                    ("Climb out the window.", Condition.IsCharacter(Characters.Pal), Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Yell at dog.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Sit on the dog bed.", Condition.IsCharacter(Characters.Mom), Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Pet dog more.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(PettingTheDogAgain)))

//        );
//        public static Event PettingTheDogAgain = new Event(nameof(PettingTheDogAgain),
//                Line("Good boy."),
//                    ("Turn away.", Effect.GoTo(nameof(LivingRoomMain))),
//                    ("Climb out the window.", Condition.IsCharacter(Characters.Pal), Effect.GoTo(nameof(TakeOffCoat))),
//                    ("Yell at dog.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Sit on the dog bed.", Condition.IsCharacter(Characters.Mom), Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Pet dog more.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(PettingTheDogAgain)))

//        );
//        public static Event SittingOnDogBed = new Event(nameof(SittingOnDogBed),
//                Line("That dog was such a piece of shit. He loved my Mom, and everyone else was not to be trusted. But he especially hated me. I'll never figure that one out. Did I kick him at some point? Whatever."),
//                    ("Cry", Effect.GoTo(nameof(CryingOnDogBed))),
//                    ("Meditate.", Effect.GoTo(nameof(Vestibule))),
//                    ("Stand up.", Effect.GoTo(nameof(LivingRoomFromDogBed))),
//                    ("Pet dog.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(DogBed)))

//        );

//        public static Event CryingOnDogBed = new Event(nameof(CryingOnDogBed),
//                Line("Ah. That... felt good."),
//                    ("Go to the kitchen.", Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Sit on the dog bed.", Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Pet dog.", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(DogBed)))

//        );

//        public static Event OnTheCouch = new Event(nameof(OnTheCouch),
//                Line("That dog was such a piece of shit. He loved my Mom, and everyone else was not to be trusted. But he especially hated me. I'll never figure that one out. Did I kick him at some point? Whatever."),
//                    ("Cry", Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Grow beard.", (state) => state.BeardLength++),
//                    ("Get up.", Effect.GoTo(nameof(LivingRoomFromCouch)))

//        );
//        public static Event MeditatingOnTheCouch = new Event(nameof(MeditatingOnTheCouch),
//                Line("...").Typewrite(),
//                    ("Cry", Effect.GoTo(nameof(CryingOnDadsChair))),
//                    ("Grow beard.", (state) => state.BeardLength++),
//                    ("Meditate", Effect.GoTo(nameof(MeditatingOnTheCouch))),
//                    ("Get up.", Effect.GoTo(nameof(LivingRoomFromCouch)))

//        );

//        public static Event OnDadsChair = new Event(nameof(OnDadsChair),
//                Line("Dad used to sit here every moment he could. I've been too afraid to sit in it. But I'm in my 40s, so there is no chance of a whipping. And yes, he's dead...."),
//                    ("Cry", Effect.GoTo(nameof(CryingOnDadsChair))),
//                    ("Meditate", Effect.GoTo(nameof(MeditatingOnDadsChair))),
//                    ("Watch Jeopardy.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(Jeopardy))),
//                    ("Get up.", Effect.GoTo(nameof(LivingRoomFromCouch)))

//        );

//        public static Event MeditatingOnDadsChair = new Event(nameof(MeditatingOnDadsChair),
//                Line("...").Typewrite(),
//                    ("Cry", Effect.GoTo(nameof(CryingOnDadsChair))),
//                    ("Meditate", Effect.GoTo(nameof(CryingOnDadsChair))),
//                    ("Watch Jeopardy.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(Jeopardy))),
//                    ("Get up.", Effect.GoTo(nameof(LivingRoomFromCouch)))

//        );

//        public static Event CryingOnDadsChair = new Event(nameof(CryingOnDadsChair),
//                Line("Why was he such a tyrant? Why did he need to make my life so miserable?"),
//                    ("Cry", Effect.GoTo(nameof(CryingOnDadsChair))),
//                    ("RecallTheTao", Effect.GoTo(nameof(PhilosophizingOnDadsChair))),
//                    ("Watch Jeopardy.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(Jeopardy))),
//                    ("Get up.", Effect.GoTo(nameof(LivingRoomMain)))

//        );

//        public static Event PhilosophizingOnDadsChair = new Event(nameof(PhilosophizingOnDadsChair),
//                Line(@"""Behave simply and hold on to purity. Lessen selfishness and restrain desires. Abandon knowledge and your worries are over."" That is so the opposite of my dad. Oh, well."),
//                    ("Meditate", Effect.GoTo(nameof(MeditatingOnDadsChair))),
//                    ("Watch Jeopardy.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(Jeopardy))),
//                    ("Get up.", Effect.GoTo(nameof(LivingRoomMain)))

//        );

//        public static Event Jeopardy = new Event(nameof(Jeopardy),
//                Line("This ancient Chinese philosopher is considered the father of Taoism."),
//                    ("Who is Lau Tzu?", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Lau Tzu.", Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Warren Buffet.", Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("A dog!", Condition.IsCharacter(Characters.Dog), Effect.GoTo(nameof(StopJeopardy))),
//                    ("Turn it off.", Effect.GoTo(nameof(StopJeopardy)))

//        );

//        public static Event StopJeopardy = new Event(nameof(StopJeopardy),
//                Line("That dog was such a piece of shit. He loved my Mom, and everyone else was not to be trusted. But he especially hated me. I'll never figure that one out. Did I kick him at some point? Whatever."),
//                    ("Cry", Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Go to the kitchen.", Effect.GoTo(nameof(TakeOffBoots))),
//                    ("Sit on the dog bed.", Effect.GoTo(nameof(SittingOnDogBed))),
//                    ("Watch Jeopardy.", Condition.IsCharacter(Characters.Dad), Effect.GoTo(nameof(Jeopardy)))

//        );

//        public static Event OutTheWindow = new Event(nameof(OutTheWindow),
//               Line("Can't get out of this place fast enough."),
//                   ("Enter House", Effect.GoTo(nameof(Vestibule))),
//                   ("Embrace the Apocalypse.", Effect.GoTo(nameof(Apocalypse)))

//       );

//        public static Event Apocalypse = new Event(nameof(Apocalypse),
//               Line("Your sickness overcomes you. The plague that conquered earth now conquers you. At least you are going out where it all started. This is the Way, I guess."),
//                   ("Restart", Effect.GoTo(nameof(Vestibule)))

//       );


//    }
//}