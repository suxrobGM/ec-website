namespace EC_Website.Web.Authorization
{
    public static class Policies
    {
        public const string HasAdminRole = "HasAdminRole";
        public const string CanManageBlogs = "CanManageBlogs";
        public const string CanManageForums = "CanManageForums";
        public const string CanManageWikiPages = "CanManageWikiPages";
        public const string CanBanUsers = "CanBanUsers";
    }
}
