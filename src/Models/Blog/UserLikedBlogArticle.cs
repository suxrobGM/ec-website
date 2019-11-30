using EC_Website.Models.UserModel;

namespace EC_Website.Models.Blog
{
    public class UserLikedBlogArticle
    {
        public string ArticleId { get; set; }
        public virtual BlogArticle Article { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
