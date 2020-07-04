namespace EC_Website.Core.Entities.BlogModel
{
    public class BlogTag
    {
        public virtual Blog Blog { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
