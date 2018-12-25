using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EC_WebSite.Models;

namespace EC_WebSite.ViewModels
{
    public class ForumThreadViewModel
    {
        public string ThreadId { get; set; }
        public string ThreadName { get; set; }
        public string SearchText { get; set; }

        [DataType(DataType.MultilineText)]
        public string NewPostText { get; set; }
        public List<PostPages> Pages { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class PostPages
    {
        public int PageNumber { get; set; }
        public List<Post> Posts { get; set; }
    }
}
