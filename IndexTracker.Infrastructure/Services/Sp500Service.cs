using HtmlAgilityPack;
using Indextracker2.Application.Models;
using Indextracker2.Application.Services;
using System.Net.Http;
using System.Globalization;
using System.Linq;

namespace Indextracker2.Infrastructure.Services
{
    public class Sp500Service : ISp500Service
    {
        private static readonly HttpClient HttpClient = new();
        static Sp500Service() => HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");

        public async Task<Sp500ValueDto> GetCurrentValueAsync(CancellationToken cancellationToken = default)
        {
            var html = await HttpClient.GetStringAsync("https://finance.yahoo.com/quote/%5EGSPC", cancellationToken);
            var value = ParseSp500Value(html);
            return new Sp500ValueDto { Timestamp = DateTime.UtcNow, Value = value };
        }

        private static decimal ParseSp500Value(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            
            var node = doc.DocumentNode.SelectSingleNode("//*[@data-testid='qsp-price']") ?? throw new InvalidOperationException("Failed to scrape S&P 500 value");

            var text = new string(node.InnerText.Trim().Where(c => char.IsDigit(c) || c is '.' or ',').ToArray());

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) || decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out value))
            {
                return value;
            }
            throw new InvalidOperationException($"Failed to parse S&P 500 value: '{text}'");
        }
    }
}
