using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Hubs
{
    public class ApplicationHub : Hub
    {
        private readonly RealTimeDataContext _context;

        public ApplicationHub(RealTimeDataContext context)
        {
            _context = context;
        }

        public override Task OnConnectedAsync()
        {
            _context.OnlineUsers.Add(Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _context.OnlineUsers.Remove(Context.User.Identity.Name);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
