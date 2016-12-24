using System;

namespace Prospector.Domain.Entities
{
    public class HoldingData
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public DateTime Date { get; set; }
        public int Shares { get; set; }
        public Decimal Price { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Commission { get; set; }
        public Decimal Levy { get; set; }
        public Decimal Percentage { get; set; }
    }
}
