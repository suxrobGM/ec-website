using EC_WebSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.ViewModels
{
    public class IndexViewModel
    {           
        public string SearchText { get; set; }
        public string SelectedFavoriteThreadId { get; set; }
        public string SelectedForumHeadId { get; set; }
        public string SelectedBoardId { get; set; }
        public IEnumerable<ForumHead> ForumHeads { get; set; }
        public IEnumerable<FavoriteThread> FavoriteThreads { get; set; }
    }
}
