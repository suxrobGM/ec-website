using System.Collections.Generic;

namespace EC_Website.Data
{
    public class RealTimeDataContext
    {
        private static RealTimeDataContext _instance;

        private RealTimeDataContext()
        {
            OnlineUsers = new List<string>();
        }

        public static RealTimeDataContext Instance => _instance ??= new RealTimeDataContext();

        public List<string> OnlineUsers { get; set; }
    }
}
