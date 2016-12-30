using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prospector.Presentation.ViewModels
{
    public class CalculatorViewModel
    {
        [Required(ErrorMessage = "Please enter an Investment Amount", AllowEmptyStrings = false)]
        [Range(1, 100000000, ErrorMessage = "Value cannot be zero")]
        [DataType(DataType.Currency)]
        [DisplayName("Investment (£)")]
        public Decimal Investment { get; set; }

        [Required(ErrorMessage = "Please enter a Price", AllowEmptyStrings = false)]
        [Range(1, 10000, ErrorMessage = "Value cannot be zero")]
        [DataType(DataType.Currency)]
        [DisplayName("Price (£)")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the Tax Amount", AllowEmptyStrings = false)]
        [Range(1, 100, ErrorMessage = "Value cannot be zero")]
        [DataType(DataType.Currency)]
        [DisplayName("Tax (£)")]
        public Decimal Tax { get; set; }

        [Required(ErrorMessage = "Please enter the Commission", AllowEmptyStrings = false)]
        [Range(5.95, 11.95, ErrorMessage = "Value cannot be zero")]
        [DataType(DataType.Currency)]
        [DisplayName("Commission (£)")]
        public Decimal Commission { get; set; }

        [Required(ErrorMessage = "Please enter the PTM Levy Amount", AllowEmptyStrings = false)]
        [DataType(DataType.Currency)]
        [DisplayName("PTM Levy (£)")]
        public Decimal Levy { get; set; }

        [Required(ErrorMessage = "Please enter the Profit Percentage", AllowEmptyStrings = false)]
        [Range(1, 100, ErrorMessage = "Value cannot be zero")]
        [DisplayName("Profit (%)")]
        public Decimal Profit { get; set; }

        [DisplayName("Cost (£)")]
        public Decimal Cost { get; set; }
        public int Shares { get; set; }

        [DisplayName("Break Even Price (£)")]
        public Decimal BreakEvenPrice { get; set; }

        [DisplayName("Profit Price (£)")]
        public Decimal ProfitPrice { get; set; }

        [DisplayName("Earnings (£)")]
        public Decimal Earnings { get; set; }
    }
}
