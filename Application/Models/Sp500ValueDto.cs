using Indextracker2.Domain.Entities;

namespace Indextracker2.Application.Models
{
    public class Sp500ValueDto
    {
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}
