using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.ForumModel
{
    public class Board
    {
        public Board()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }

        [StringLength(20)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter the board name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Title { get; set; }

        [StringLength(80)]
        public string Slug { get; set; }
        public DateTime Timestamp { get; set; }

        [StringLength(20)]
        public string ForumId { get; set; }
        public virtual ForumHead Forum { get; set; }

        public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
