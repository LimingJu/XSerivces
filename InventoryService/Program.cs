using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using SharedConfig;
using SharedModel;

namespace InventoryService
{
    public class Program
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

            //var Initializes = new CreateDatabaseIfNotExists<ApplicationDbContext>();
            //using (var db = new ApplicationDbContext())
            //{
            //    Initializes.InitializeDatabase(db);
            //}

            Console.WriteLine("Starting listening: " + baseAddress);
            try
            {
                
                // Start OWIN host 
                using (WebApp.Start<SignalRStartup>(url: baseAddress))
                {
                    // Create HttpCient and make a request to api/values 
                    //HttpClient client = new HttpClient();

                    //var response = client.GetAsync(baseAddress + "/api/ShowMeApi").Result;

                    //Console.WriteLine("Accessing one page for testing...");
                    //Console.WriteLine("     returned Http Status code: " + ((int)(response.StatusCode)).ToString());
                    //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    logger.Info("Started successfully");
                    Console.WriteLine("Started successfully");

                    RunAsync().Wait();
                    while (true)
                    {                        
                        Console.ReadLine();
                    }                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start, exception detail: " + ex);
                logger.Error("Started failed with error: " + ex);
            }
            
        }

        // track id of lasteast updating  
        private static int _lastPosItemId = 0;

        /// <summary>
        /// check db data change for every 10 seconds, assuming Id increases as new items are added
        /// </summary>
        /// <returns></returns>
        private static Task RunAsync()
        {
            while (true)
            {
                try
                {
                    using (var db = new DefaultAppDbContext())
                    {
                        // Check db change, ignore the first time on startup
                        if (db.PosItemModels.Select(x => x.Id).Max() > _lastPosItemId && _lastPosItemId > 0)
                        {
                            _lastPosItemId = db.PosItemModels.Select(x => x.Id).Max();

                            var context = GlobalHost.ConnectionManager.GetHubContext<InventoryHub>();
                            context.Clients.All.NotifyUpdate(db.PosItemModels, db.SnapShotModels);
                        }
                    }

                    System.Threading.Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    if (ex is DbException)
                    {
                        logger.Error("Wrong data source {0}", ex);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
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