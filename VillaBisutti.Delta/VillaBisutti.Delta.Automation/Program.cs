using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VillaBisutti.Delta.Core.Configuration;

namespace VillaBisutti.Delta.Automation
{
	public static class Program
	{
		public static Dictionary<Watcher, Thread> Services { get; set; }
		public static void StartServicesRunner()
		{
			//Registrar todos os watchers
			//Watcher watcher = Watcher.Factory("WatcherBoasVindas");
			//watcher.Run(null);
		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e != null && e.ExceptionObject != null)
            {
                //TODO: Log
                //logger.Error(e.ExceptionObject as Exception);

            }
        }
	}   
}
