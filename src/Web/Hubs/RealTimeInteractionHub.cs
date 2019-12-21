using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using EC_Website.Data;

namespace EC_Website.Hubs
{
    public class RealTimeInteractionHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            RealTimeDataContext.Instance.OnlineUsers.Add(Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            RealTimeDataContext.Instance.OnlineUsers.Remove(Context.User.Identity.Name);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
