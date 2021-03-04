using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace WebDay2021.SignalR.SensorDashboard.SensorConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("WebDay 2021 - SignalR : Fake Sensor Console");
            
            var sensorId = Guid.NewGuid();

            Console.WriteLine($"SensorId: {sensorId}");

            await using var hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl("https://localhost:44391/sensorhub")
                .Build();

            await hubConnection.StartAsync();

            await hubConnection.SendAsync("ConnectSensor", sensorId.ToString());

            Console.CancelKeyPress += new ConsoleCancelEventHandler(async (sender, args) => {
                await hubConnection.SendAsync("DisconnectSensor", sensorId.ToString());
                await hubConnection.StopAsync();

                Console.WriteLine("\nThe publish operation has been interrupted.");
            });

            Console.WriteLine("\nPress CRTL+C to exit...");

            var random = new Random();

            while (true)
            {
                var measure = random.Next(20, 22) + random.NextDouble();
                
                await hubConnection.SendAsync("PushSensorMeasurements", sensorId, measure);

                Console.WriteLine($"Pushed: {measure}");
                
                await Task.Delay(1000);
            }            
        }
    }
}
