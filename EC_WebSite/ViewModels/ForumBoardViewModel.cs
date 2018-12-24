using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;

namespace EC_WebSite.ViewModels
{
    public class ForumBoardViewModel
    {
        public ForumBoardViewModel()
        {
            Threads = new List<Thread>();
        }

        public string SearchText { get; set; }

        public Board Board { get; set; }

        public List<Thread> Threads { get; set; } 
    }
}
