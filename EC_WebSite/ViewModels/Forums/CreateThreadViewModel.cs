﻿using EC_WebSite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.ViewModels
{
    public class CreateThreadViewModel
    {
        public string BoardId { get; set; }
        public string BoardName { get; set; }
        public string Topic { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}