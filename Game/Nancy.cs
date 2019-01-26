using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
	public class RequestObject
	{
		public int Id { get; set; }
	}

	public class Nancy : NancyModule
	{
		public Nancy()
		{

			Get["/"] = _ => "Hello Kwame!";
			Get["/Kwame&id={id}"] = value =>
			{
				var request = this.Bind<RequestObject>();
				return "bong " + (request.Id + 1);
			};
			
		}
	}
}
