using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Data
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
