using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebDay2021.SignalR.SimpleFruitShop.WebApp.Hubs
{
    [Authorize]
    public class MonitorHub : Hub
    {

    }
}
