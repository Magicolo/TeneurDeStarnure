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
		static void Main(string[] args)
		{
			HostConfiguration hostConfigs = new HostConfiguration();
			//hostConfigs.UrlReservations.CreateAutomatically = true;
			hostConfigs.RewriteLocalhost = true;
			using (var host = new NancyHost(new Uri("http://localhost:6112"), new DefaultNancyBootstrapper(), hostConfigs))
			{
				host.Start();
				Console.WriteLine("Running on http://192.168.1.203:8080");
				Console.ReadLine();
			}
		}
	}
}
