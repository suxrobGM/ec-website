using System;

namespace EC_Website.Data
{
    public class SingletonContext
    {
        private static SingletonContext _instance;

        private SingletonContext()
        {

        }

        public static SingletonContext Instance
        {
            get
            {
                if (_instance == null)                
                    _instance = new SingletonContext();
                
                return _instance;
            }
        }

        public int OnlineUsersCount { get; set; }
    }
}
