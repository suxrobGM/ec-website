using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models.UserModel;

namespace EC_WebSite.Models.ForumModel
{
    public class FavoriteThread
    {
        public string ThreadId { get; set; }        
        public virtual Thread Thread { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
