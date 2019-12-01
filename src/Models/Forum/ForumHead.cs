using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;

namespace EC_Website.Models.ForumModel
{
    public class ForumHead
    {
        public ForumHead()
        {
            Id = GeneratorId.GenerateLong();
            Boards = new List<Board>();
            Timestamp = DateTime.Now;
        }
       
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
    }
}
