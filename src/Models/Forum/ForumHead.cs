using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;

namespace EC_WebSite.Models.ForumModel
{
    public class ForumHead
    {
        public ForumHead()
        {
            Id = GeneratorId.GenerateLong();
            Boards = new List<Board>();
        }
       
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
    }
}
