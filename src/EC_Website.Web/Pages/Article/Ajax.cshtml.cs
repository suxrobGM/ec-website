using System.Linq;
using System.Threading.Tasks;
using EC_Website.Core.Entities.Blog;
using EC_Website.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_Website.Web.Pages.Article
{
    public class AjaxModel : PageModel
    {
        private readonly IRepository _repository;

        public AjaxModel(IRepository repository)
        {
            _repository = repository;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetLikeArticleAsync(string articleId)
        {
            var article = await _repository.GetByIdAsync<BlogEntry>(articleId);

            if (article == null)
            {
                return BadRequest($"Specified article with {articleId} could not be found");
            }

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

            await _repository.UpdateAsync(article);
            return new OkObjectResult(article.LikedUserNames.Count);
        }
    }
}