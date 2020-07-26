using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Infrastructure.Repositories
{
    public class WikiRepository : Repository, IWikiRepository
    {
        private readonly ApplicationDbContext _context;

        public WikiRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
