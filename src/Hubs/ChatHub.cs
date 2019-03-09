using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using EC_WebSite.Data;

namespace EC_WebSite.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            SingletonContext.Instance.OnlineUsersCount++;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (SingletonContext.Instance.OnlineUsersCount > 0)          
                SingletonContext.Instance.OnlineUsersCount--;          
            
            return base.OnDisconnectedAsync(exception);
        }
    }
}
