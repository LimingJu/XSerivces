using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNet.SignalR.Client;
using SharedConfig;

namespace PosItemsClient
{
    class Program
    {
        static ILog logger = log4net.LogManager.GetLogger("Main");
        private static PosItemsClient client;

        static void Main()
        {           
            //var initializes = new CreateDatabaseIfNotExists<DefaultAppDbContext>();
            //using (var db = new DefaultAppDbContext())
            //{
            //    initializes.InitializeDatabase(db);
            //}

            RunAsync().Wait();

        }

        static async Task RunAsync()
        {
            string uri = ConfigurationManager.AppSettings["ClientSettingsProvider.ServiceUri"];

            var connection = new HubConnection(uri);

            connection.TraceLevel = TraceLevels.All;
            connection.TraceWriter = Console.Out;
            connection.StateChanged += OnConnectionStateChanged;
            connection.Received += OnReceived;

            var hubProxy = connection.CreateHubProxy("InventoryPublisher");

            client = new PosItemsClient(hubProxy, logger);

            while (true)
            {
                try
                {
                    if (connection.State != ConnectionState.Connected)
                    await connection.Start();

                }
                catch (Exception ex)
                {
                    if (ex is HttpRequestException)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }

        }

        private static void OnReceived(string obj)
        {
            Console.WriteLine("received message");
        }

        private static void OnConnectionStateChanged(StateChange obj)
        {
            if (obj.NewState == ConnectionState.Connected)
            {
                int snapshot = -1;

                using (var db = new DefaultAppDbContext())
                {
                    if (db.PosItemModels.Any())
                        snapshot = db.PosItemModels.OrderBy(x=>x.Id).First().SnapShotId;
                }

                // Request items at the startup
                client.Get(snapshot);
            }
        }
    }
}
