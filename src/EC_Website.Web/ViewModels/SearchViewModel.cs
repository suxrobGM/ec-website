using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EC_Website.Web.ViewModels
{
    public enum SearchPageType
    {
        Threads,
        Posts
    }

    public enum SearchType
    {
        Everywhere,
        Here
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
            SearchPageType = SearchPageType.Threads;
            SearchType = SearchType.Everywhere;
            SearchTimeFrame = SearchTimeFrame.AllTime;
        }

        public string SearchString { get; set; }
        public string UserName { get; set; }
        public SearchPageType SearchPageType { get; set; }
        public SearchType SearchType { get; set; }
        public SearchTimeFrame SearchTimeFrame { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append($"SearchString={SearchString}");
            if (UserName != null)
            {
                builder.Append($"&UserName={UserName}");
            }
            builder.Append($"&SearchPageType={SearchPageType}");
            builder.Append($"&SearchType={SearchType}");
            builder.Append($"&SearchTimeFrame={SearchTimeFrame}");
            return builder.ToString();
        }
    }
}
