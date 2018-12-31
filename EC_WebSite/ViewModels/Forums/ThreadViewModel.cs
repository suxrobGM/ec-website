using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Identity;

namespace EC_WebSite.ViewModels
{
    public class ThreadViewModel
    {
        private UserManager<User> _userManager;

        public ThreadViewModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string SearchText { get; set; }
        public string SelectedPostId { get; set; }
        public Thread Thread { get; set; }

        [DataType(DataType.MultilineText)]
        public string NewPostText { get; set; }               
        public IEnumerable<Post> Posts { get; set; }

        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }  
}
