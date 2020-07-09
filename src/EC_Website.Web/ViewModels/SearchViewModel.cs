using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EC_Website.Web.ViewModels
{
    public enum SearchPageType
    {
        [Display(Name = "Threads")]
        Threads,

        [Display(Name = "Posts")]
        Posts
    }

    public enum SearchTimeFrame
    {
        [Display(Name = "All Time")]
        AllTime,

        [Display(Name = "Last Day")]
        LastDay,

        [Display(Name = "Last Week")]
        LastWeek,

        [Display(Name = "Last Month")]
        LastMonth,

        [Display(Name = "Last Year")]
        LastYear
    }

    public class SearchViewModel
    {
        public SearchViewModel()
        {
            PageType = SearchPageType.Threads;
            TimeFrame = SearchTimeFrame.AllTime;
        }

        public string SearchString { get; set; }
        public string UserName { get; set; }
        public SearchPageType PageType { get; set; }
        public SearchTimeFrame TimeFrame { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"SearchString={SearchString}");
            if (UserName != null)
            {
                builder.Append($"&UserName={UserName}");
            }
            builder.Append($"&PageType={PageType}");
            builder.Append($"&TimeFrame={TimeFrame}");
            return builder.ToString();
        }
    }
}
