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
			Get["/"] = parameters =>
			{
				return Response.AsFile("www/index.html", "text/html");
			};

			Get["/Kwame/{id}"] = value =>
			{
				var request = this.Bind<RequestObject>();
				return JsonResponse("{ type: \"text\", text: \"just kidding"+ (request.Id + 1)+"\" }");
				//return "bong " + (request.Id + 1);
			};
			
		}

		public Response JsonResponse(string jsonString) {
			byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
			return new Response()
			{
				StatusCode = HttpStatusCode.OK,
				ContentType = "application/json",
				//ReasonPhrase = "Because why not!",
				Headers = new Dictionary<string, string>()
				{
					{ "Content-Type", "application/json" }
					//{ "X-Custom-Header", "Sup?" }
				},
				Contents = c => c.Write(jsonBytes, 0, jsonBytes.Length)
			};
		}
	}
}
