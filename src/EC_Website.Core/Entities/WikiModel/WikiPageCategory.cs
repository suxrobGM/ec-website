namespace EC_Website.Core.Entities.WikiModel
{
    public class WikiPageCategory
    {
        public virtual WikiPage WikiPage { get; set; }
        public virtual Category Category { get; set; }
    }
}
