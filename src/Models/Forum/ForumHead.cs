using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.ForumModel
{
    public class ForumHead
    {
        public ForumHead()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }

        [StringLength(32)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter the forum head name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}
