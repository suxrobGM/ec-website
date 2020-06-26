using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Article
{
    public class AjaxModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AjaxModel(ApplicationDbContext context)
        { 
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetLikeArticleAsync(string articleId)
        {
            var article = await _context.BlogEntries.FirstAsync(i => i.Id == articleId);
            var username = User.Identity.Name;
            var likedUserNamesList = article.LikedUserNames.ToList();

            if (!likedUserNamesList.Contains(username))
            {
                likedUserNamesList.Add(username);
                article.LikedUserNames = likedUserNamesList;
            }
            else
            {
                likedUserNamesList.Remove(username);
                article.LikedUserNames = likedUserNamesList;
            }

            await _context.SaveChangesAsync();
            return new OkObjectResult(article.LikedUserNames.Count);
        }
    }
}