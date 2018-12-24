using EC_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.ViewModels
{
    public class CreateThreadViewModel
    {
        public Board Board { get; set; }
        public string Topic { get; set; }
        public Post Post { get; set; }      
        public string Text {get; set;}
    }
}
