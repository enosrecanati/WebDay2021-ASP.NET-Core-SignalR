using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebDay2021.SignalR.SensorDashboard.WebApp.Hubs;

namespace WebDay2021.SignalR.SensorDashboard.WebApp.Services
{
    public class SensorService
    {
        private readonly ConcurrentDictionary<string, string> _sensors;

        private readonly IHubContext<SensorHub> _hubContext;

        public SensorService(IHubContext<SensorHub> hubContext)
        {
            _sensors = new ConcurrentDictionary<string, string>();
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task ConnectSensorAsync(string sensorId, string clientId)
        {
            _ = _sensors.AddOrUpdate(sensorId, clientId, (key, oldValue) => clientId);
            
            await _hubContext.Clients.All.SendAsync("SensorConnected", sensorId);
        }

        public async Task DisconnectSensorAsync(string sensorId)
        {
            _sensors.Remove(sensorId, out _);

            await _hubContext.Clients.All.SendAsync("SensorDisconnected", sensorId);
        }

        public IEnumerable<string> GetSensors()
        {
            return _sensors.Keys;
        }
    }
}
