using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Owin.Hosting;
using SharedConfig;

namespace TransactionService
{
    class Program
    {
        static ILog logger = log4net.LogManager.GetLogger("Main");
        static ILog perfLogger = log4net.LogManager.GetLogger("Performance");
        private static System.Timers.Timer perfTimer;
        static void Main(string[] args)
        {
            //AreaRegistration.RegisterAllAreas(); //AreaRegistration.RegisterAllAreas(); AreaRegistration.RegisterAllAreas();
            perfTimer = new System.Timers.Timer();
            perfTimer.Interval = 300 * 1000;
            perfTimer.Elapsed += (_, __) => LogMemory();
            perfTimer.Start();
            log4net.Config.XmlConfigurator.Configure();

            string baseAddress = ConfigurationManager.AppSettings["ListeningEndPoint"];

            logger.Info("Starting listening: " + baseAddress);
            Console.WriteLine("Starting listening: " + baseAddress);
            try
            {

                // Start OWIN host 
                using (WebApp.Start<SelfHostStartup>(url: baseAddress))
                {
                    // Create HttpCient and make a request to api/values 
                    //HttpClient client = new HttpClient();

                    //var response = client.GetAsync(baseAddress + "/api/ShowMeApi").Result;

                    //Console.WriteLine("Accessing one page for testing...");
                    //Console.WriteLine("     returned Http Status code: " + ((int)(response.StatusCode)).ToString());
                    //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    logger.Info("Started successfully");
                    Console.WriteLine("Started successfully");
                    while (true)
                        Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start, exception detail: " + ex);
                logger.Error("Started failed with error: " + ex);
            }

            while (true)
                Console.ReadLine();
        }

        /// <summary>
        /// Logs the current memory status.
        /// </summary>
        private static void LogMemory()
        {
            //General process information
            using (var thisProcess = Process.GetCurrentProcess())
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                AssemblyName assemblyName = entryAssembly?.GetName();

                perfLogger.Info(string.Format("Process {0} has been running since {1} on machine \"{2}\"\r\n" +
                                              "{3}" +
                                              "   Total ProcessorTime={4}\r\n" +
                                              "   PagedMemory={5}kB, PagedSystemMemory={6}kB, VirtualMemory={7}kB, WorkingSet={8}kB, " +
                                              "   Total managed memory={9}kB\r\n" +
                                              "   HandleCount={10}, ThreadCount={11}",
                    thisProcess.ProcessName, thisProcess.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), thisProcess.MachineName,
                    assemblyName != null ? string.Format("   Entry assembly: {0}\r\n", assemblyName.FullName) : "",
                    thisProcess.TotalProcessorTime.ToString(),
                    thisProcess.PagedMemorySize64 / 1024, thisProcess.PagedSystemMemorySize64 / 1024, thisProcess.VirtualMemorySize64 / 1024,
                    thisProcess.WorkingSet64 / 1024,
                    GC.GetTotalMemory(false) / 1024,
                    thisProcess.HandleCount,
                    thisProcess.Threads.Count));
            }
        }
    }
}
