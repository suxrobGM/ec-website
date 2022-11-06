using System.Collections.Generic;

namespace EC_Website.Infrastructure.Data;

public class RealTimeDataContext
{
    public RealTimeDataContext()
    {
        OnlineUsers = new List<string>();
    }

    public List<string> OnlineUsers { get; }
}