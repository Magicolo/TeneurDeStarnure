using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
	class Program
	{
		private static string _url = "http://localhost";
		private static int _port = 6114;
		static void Main(string[] args)
		{
			HostConfiguration hostConfigs = new HostConfiguration();
			hostConfigs.UrlReservations.CreateAutomatically = true;
			hostConfigs.RewriteLocalhost = true;
			var uri = new Uri($"{_url}:{_port}/");
			using (var host = new NancyHost(uri, new DefaultNancyBootstrapper(), hostConfigs))
			{
				host.Start();
				Console.WriteLine($"Started listennig port {_port}");
				Console.ReadLine();
			}
		}
	}
}
