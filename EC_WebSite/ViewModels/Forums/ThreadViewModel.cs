using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;

namespace EC_WebSite.ViewModels
{
    public class ThreadViewModel
    {       
        public string SearchText { get; set; }
        public string SelectedPostId { get; set; }
        public Thread Thread { get; set; }

        [DataType(DataType.MultilineText)]
        public string NewPostText { get; set; }
        public IEnumerable<PostPages> Pages { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }

    public class PostPages
    {
        public int PageNumber { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
