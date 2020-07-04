using System;
using System.Linq;
using System.Threading.Tasks;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Infrastructure.Repositories
{
    public class BlogRepository : Repository, IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddTagsAsync(Blog blog, bool saveChanges = true, params Tag[] tags)
        {
            foreach (var tag in tags)
            {
                var originTag = await GetAsync<Tag>(i => string.Equals(i, tag, StringComparison.CurrentCultureIgnoreCase));

                if (originTag == null)
                {
                    await _context.Set<Tag>().AddAsync(new Tag(tag));
                }
                else
                {
                    originTag = new Tag(tag);
                }

                if (blog.BlogTags.Any(i => string.Equals(i.Tag, originTag, StringComparison.CurrentCultureIgnoreCase)))
                {
                    continue;
                }

                blog.BlogTags.Add(new BlogTag
                {
                    Tag = originTag
                });
            }

            if (saveChanges)
            {
                await UpdateAsync(blog);
            }
        }

        public Task AddLikeAsync(Blog blog, ApplicationUser user)
        {
            if (blog == null || user == null)
            {
                return Task.CompletedTask;
            }

            if (blog.LikedUsers.All(i => i.Id != user.Id))
            {
                blog.LikedUsers.Add(user);
            }

            return UpdateAsync(blog);
        }

        public Task DeleteTagsAsync(Tag[] tags)
        {
            var blogTags = _context.Set<BlogTag>().Where(i => tags.Contains(i.Tag));
            _context.RemoveRange(blogTags);
            _context.Set<Tag>().RemoveRange(tags);
            return _context.SaveChangesAsync();
        }

        public Task RemoveLikeAsync(Blog blog, ApplicationUser user)
        {
            if (blog == null || user == null)
            {
                return Task.CompletedTask;
            }

            blog.LikedUsers.Remove(user);
            return UpdateAsync(blog);
        }

        public Task RemoveBlogTagsAsync(Blog blog, params Tag[] tags)
        {
            var blogTags = _context.Set<BlogTag>().Where(i => tags.Contains(i.Tag) && i.Blog.Id == blog.Id);
            _context.RemoveRange(blogTags);
            return _context.SaveChangesAsync();
        }
    }
}
