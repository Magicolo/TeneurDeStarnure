using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	public static class Kevin
	{
		public static string GetCurrentEventId(string playerId)
		{
			return "a";
		}

		public static string GetCurrentEvent(string playerId) 
		{
			return "{nodes =[{\"type\"=\"text\"}]}"; 
		}

		public static string GetTestContent() 
		{ 
			return "{nodes =[{\"type\"=\"HelloWorld\"}]}";
		}

		public static string HandleAnswer(string id, string answerId)
		{
			return "OK";
		}
	}
}
