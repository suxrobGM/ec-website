﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_Website.Pages.Admin.Auth
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}