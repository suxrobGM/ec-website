using System;

namespace VkDonate.Api
{
    public class Donation
    {
        public ulong Id { get; set; }
        public ulong Uid { get; set; }
        public DateTime Date { get; set; }
        public ulong Sum { get; set; }
        public string Msg { get; set; }
        public bool Anon { get; set; }
        public bool Visible { get; set; }
    }

    public enum Sort
    {
        Date,
        Sum
    }

    public enum Order
    {
        Ascending,
        Descending
    }
}
