using EC_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.ViewModels
{
    public class ForumsViewModel
    {
        private ApplicationDbContext db;

        public ForumsViewModel()
        {
            db = new ApplicationDbContext();
            ForumHeaders = new List<ForumHeader>(db.ForumHeaders);
        }

        public string SearchText { get; set; }
        public List<ForumHeader> ForumHeaders { get; set; }
    }
}
