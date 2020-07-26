using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Infrastructure.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context) : base(context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task UpdateUserRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var actualRoles = roles.ToList();
            var previousRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, previousRoles);
            
            foreach (var role in actualRoles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task UpdateUserBadgesAsync(ApplicationUser user, IEnumerable<string> badges)
        {
            user.UserBadges.Clear();

            foreach (var badgeName in badges)
            {
                var badge = await GetAsync<Badge>(i => i.Name.ToLower() == badgeName.ToLower());

                if (badge != null)
                {
                    user.UserBadges.Add(new UserBadge()
                    {
                        Badge = badge
                    });
                }
            }

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteDeeplyAsync(ApplicationUser user)
        {
            if (user == null)
            {
                return;
            }

            var deletedUserAccount = await _userManager.FindByNameAsync("DELETED_USER");
            var threads = _context.Set<Thread>().Where(i => i.Author.Id == user.Id);
            var posts =  _context.Set<Post>().Where(i => i.Author.Id == user.Id);
            var articles =  _context.Set<Blog>().Where(i => i.Author.Id == user.Id);
            var comments =  _context.Set<Comment>().Where(i => i.Author.Id == user.Id);
            var wikiPages = _context.Set<WikiPage>().Where(i => i.Author.Id == user.Id);

            foreach (var thread in threads)
            {
                thread.Author = deletedUserAccount;
            }

            foreach (var post in posts)
            {
                post.Author = deletedUserAccount;
            }

            foreach (var article in articles)
            {
                article.Author = deletedUserAccount;
            }

            foreach (var comment in comments)
            {
                comment.Author = deletedUserAccount;
            }

            foreach (var wikiPage in wikiPages)
            {
                wikiPage.Author = deletedUserAccount;
            }

            await _context.SaveChangesAsync();
            await _userManager.DeleteAsync(user);
        }
    }
}
