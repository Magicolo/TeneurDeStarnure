using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
	public class RequestObject
	{
		public string Id { get; set; }
		public string AnswerId { get; set; }
	}

	public class Nancy : NancyModule
	{
		public Nancy()
		{
			//Serve player's Webpage
			Get["/"] = parameters => Response.AsFile("www/index.html", "text/html");


			//Player select
			Get["/getCharacters"] = _ => JsonResponse(Kevin.GetCharacters());
			Get["/GetNewPlayerId/{id}"] = parameters => JsonResponse(Kevin.GetNewPlayerId(parameters.id));

			//Ajax
			Get["/user/{id}/getCurrentEventId"] = value =>
			{
				var request = this.Bind<RequestObject>();
				return JsonResponse(Kevin.GetCurrentEventId(request.Id));
			};

			Get["/user/{id}/getEventContent"] = value =>
			{
				var request = this.Bind<RequestObject>();
				return JsonResponse(Kevin.GetCurrentEvent(request.Id));
			};

			Get["/user/{id}/getPlayer"] = value =>
			{
				var request = this.Bind<RequestObject>();
				return JsonResponse(Kevin.GetPlayer(request.Id));
			};

			Get["/user/{id}/getTestContent"] = value =>
			{
				var request = this.Bind<RequestObject>();
				return JsonResponse(Kevin.GetTestContent());
			};


			//answer
			Get["/user/{id}/answer/{answerId}"] = value =>
			{
				var request = this.Bind<RequestObject>();
				Console.WriteLine($"Receiving answer {request.AnswerId} from {request.Id}");
				var answer = Kevin.ChooseEventChoice(request.Id, request.AnswerId);
				return JsonResponse(answer);
			};

		}

		public Response JsonResponse(Result result) => JsonResponse(result.Serialize());

		public Response JsonResponse(string jsonString)
		{
			var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
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
