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

        public async Task AddTagsAsync(Blog blog, params Tag[] tags)
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

            await UpdateAsync(blog);
        }

        public Task AddLikeAsync(Blog blog, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLikeAsync(Blog blog, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTagsAsync(Blog blog, params Tag[] tags)
        {
            foreach (var tag in tags)
            {
                blog.BlogTags.Remove(new BlogTag
                {
                    Tag = new Tag { Name = tag }
                });
            }

            return UpdateAsync(blog);
        }
    }
}
