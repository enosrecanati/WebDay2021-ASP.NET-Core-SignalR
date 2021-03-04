using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebDay2021.SignalR.SimpleMultiplatformChat.WebApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username, string message)
        {
            await Clients.Others.SendAsync("MessageReceived", username, message);
        }
    }
}
