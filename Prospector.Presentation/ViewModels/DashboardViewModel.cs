using System;

namespace Prospector.Presentation.ViewModels
{
    public class DashboardViewModel
    {
        public String Code { get; set; }
        public String Name { get; set; }
        public Decimal BreakEvenPrice { get; set; }
        public Decimal ProfitPrice { get; set; }
        public Decimal CurrentPrice { get; set; }
        public Decimal PercentageDifference { get; set; }
        public Decimal Earnings { get; set; }
    }
}
