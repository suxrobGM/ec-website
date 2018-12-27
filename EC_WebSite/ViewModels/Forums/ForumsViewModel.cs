using EC_WebSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.ViewModels
{
    public class ForumsViewModel
    {           
        public string SearchText { get; set; }
        public IEnumerable<ForumHeader> ForumHeaders { get; set; }
    }
}
