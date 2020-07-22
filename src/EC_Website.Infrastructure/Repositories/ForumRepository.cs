using System.Linq;
using System.Threading.Tasks;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Infrastructure.Repositories
{
    public class ForumRepository : Repository, IForumRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddThreadAsync(Thread thread)
        {
            thread.Slug = GetVerifiedSlug(thread.Slug);
            await _context.Set<Thread>().AddAsync(thread);
            await _context.SaveChangesAsync();
        }

        public Task AddFavoriteThreadAsync(Thread thread, ApplicationUser user)
        {
            if (thread == null || user == null)
            {
                return Task.CompletedTask;
            }

            if (thread.FavoriteThreads.All(i => i.UserId != user.Id && i.ThreadId == thread.Id))
            {
                thread.FavoriteThreads.Add(new FavoriteThread()
                {
                    Thread = thread,
                    User = user
                });
            }
            
            return UpdateAsync(user);
        }

        public Task RemoveFavoriteThreadAsync(Thread thread, ApplicationUser user)
        {
            if (thread == null || user == null)
            {
                return Task.CompletedTask;
            }

            var favoriteThread = _context.Set<FavoriteThread>().FirstOrDefault(i => i.UserId == user.Id && i.ThreadId == thread.Id);

            if (favoriteThread == null) 
                return Task.CompletedTask;

            thread.FavoriteThreads.Remove(favoriteThread);
            return UpdateAsync(thread);
        }

        public Task UpdateThreadAsync(Thread thread)
        {
            thread.Slug = GetVerifiedSlug(thread.Slug);
            return UpdateAsync(thread);
        }

        public async Task DeleteForumAsync(Forum forum)
        {
            var sourceForum = _context.Set<Forum>().FirstOrDefault(i => i.Id == forum.Id);

            if (sourceForum == null)
                return;

            foreach (var board in sourceForum.Boards)
            {
                await DeleteBoardAsync(board, false);
            }

            _context.Remove(forum);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoardAsync(Board board, bool saveChanges = true)
        {
            var sourceBoard = _context.Set<Board>().FirstOrDefault(i => i.Id == board.Id);

            if (sourceBoard == null)
                return;

            foreach (var thread in sourceBoard.Threads)
            {
                await DeleteThreadAsync(thread, false);
            }

            _context.Remove(sourceBoard);

            if (saveChanges)
                await _context.SaveChangesAsync();
        }

        public async Task DeleteThreadAsync(Thread thread, bool saveChanges = true)
        {
            var sourceThread = _context.Set<Thread>().FirstOrDefault(i => i.Id == thread.Id);

            if (sourceThread == null) return;

            foreach (var post in sourceThread.Posts)
            {
                _context.Set<Post>().Remove(post);
            }

            _context.Remove(sourceThread);

            if (saveChanges)
                await _context.SaveChangesAsync();
        }

        public Task DeletePostAsync(Post post)
        {
            var sourcePost = _context.Set<Post>().FirstOrDefault(i => i.Id == post.Id);

            if (sourcePost == null)
                return Task.CompletedTask;

            _context.Remove(sourcePost);
            return _context.SaveChangesAsync();
        }

        private string GetVerifiedSlug(string slug)
        {
            var verifiedSlug = slug;
            var hasSameSlug = _context.Set<Thread>().Any(i => i.Slug == verifiedSlug);

            var count = 0;
            while (hasSameSlug)
            {
                verifiedSlug = slug.Insert(0, $"{++count}-");
                hasSameSlug = _context.Set<Thread>().Any(i => i.Slug == verifiedSlug);
            }

            return verifiedSlug;
        }
    }
}