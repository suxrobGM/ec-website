using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkDonate.Api
{
    public class DonateClient
    {
        private readonly string _apiKey;
        private readonly string _baseAddress;
        private readonly HttpClient _client;

        public DonateClient(string apiKey)
        {
            _client = new HttpClient();
            _apiKey = apiKey;
            _baseAddress = "https://api.vkdonate.ru";
        }

        public async Task<Donation[]> GetDonationsAsync(ushort count = 10, ushort offset = 0, Sort sortBy = Sort.Date, Order orderBy = Order.Descending)
        {
            if (count > 50)
                count = 50;

            var requestUrl = new Uri($"{_baseAddress}?action=donates&count={count}&sort={GetSort(sortBy)}&order={GetOrder(orderBy)}&offset={offset}&key={_apiKey}");
            var response = await _client.GetAsync(requestUrl);
            var content = await response.Content.ReadAsStringAsync();
            var donatesJson = JObject.Parse(content).GetValue("donates").ToString();
            var donates = JsonConvert.DeserializeObject<Donation[]>(donatesJson);
            return donates;
        }

        private string GetSort(Sort sort)
        {
            switch (sort)
            {
                case Sort.Date:
                    return "date";
                case Sort.Sum:
                    return "sum";
                default:
                    return "date";
            }
        }

        private string GetOrder(Order order)
        {
            switch (order)
            {
                case Order.Ascending:
                    return "asc";
                case Order.Descending:
                    return "desc";
                default:
                    return "desc";
            }
        }
    }
}
