using System;

namespace EC_Website.Data
{
    public class SingletonContext
    {
        private static SingletonContext _instance;

        private SingletonContext()
        {

        }

        public static SingletonContext Instance => _instance ??= new SingletonContext();

        public int OnlineUsersCount { get; set; }
    }
}
