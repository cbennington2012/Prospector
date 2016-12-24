using System;

namespace Prospector.Presentation.ViewModels
{
    public class HoldingViewModel
    {
        public Guid Id { get; set; }
        public String Code { get; set; }
        public DateTime Date { get; set; }
        public int Shares { get; set; }
        public Decimal Price { get; set; }
        public Decimal Tax { get; set; }
        public Decimal Commission { get; set; }
        public Decimal Levy { get; set; }
        public Decimal Cost { get; set; }
        public Decimal BreakEvenPrice { get; set; }
        public Decimal Percentage { get; set; }
        public Decimal ProfitPrice { get; set; }
        public Decimal Earnings { get; set; }
    }
}
