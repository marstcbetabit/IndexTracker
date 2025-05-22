using IndexTracker.Domain.Entities;

namespace IndexTracker.Application.Models
{
    public class Sp500ValueDto
    {
        public DateTime Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}
