using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebDay2021.SignalR.SensorDashboard.WebApp.Services;

namespace WebDay2021.SignalR.SensorDashboard.WebApp.Hubs
{
    public class SensorHub : Hub
    {
        private const string SENSOR_GROUP = "SENSOR";
        private const string DASHBOARD_GROUP = "DASHBOARD";

        private readonly SensorService _sensorService;

        public SensorHub(SensorService sensorService)
        {
            _sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));
        }

        public async Task ConnectDashboard()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, DASHBOARD_GROUP);

            await Clients.Caller.SendAsync("DashboardConnected", _sensorService.GetSensors());
        }
        
        public async Task ConnectSensor(string sensorId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, SENSOR_GROUP);

            await _sensorService.ConnectSensorAsync(sensorId, Context.ConnectionId);
        }

        public async Task DisconnectDashboard()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, DASHBOARD_GROUP);
        }

        public async Task DisconnectSensor(string sensorId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, SENSOR_GROUP);
            
            await _sensorService.DisconnectSensorAsync(sensorId);
        }

        public async Task PushSensorMeasurements(string sensorId, double value)
        {
            await Clients.Group(DASHBOARD_GROUP).SendAsync("SensorMeasureReceived", sensorId, value);
        }
    }
}
