using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Prospector.Domain.Enumerations;

namespace Prospector.Presentation.ViewModels
{
    public class TransactionViewModel
    {
        public String Id { get; set; }

        [Required(ErrorMessage = "Please specify a Transaction Type")]
        [DisplayName("Transaction Type")]
        public TransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "Please enter the Code", AllowEmptyStrings = false)]
        public String Code { get; set; }

        [Required(ErrorMessage = "Please enter the date", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter the time", AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Please enter the amount of shares")]
        [Range(1, 1000000, ErrorMessage = "Value cannot be zero")]
        public int Shares { get; set; }

        [Required(ErrorMessage = "Please enter a Price", AllowEmptyStrings = false)]
        [Range(1, 10000, ErrorMessage = "Value cannot be zero")]
        [DataType(DataType.Currency)]
        [DisplayName("Price (£)")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the Tax Amount", AllowEmptyStrings = false)]
        [Range(0, 1000, ErrorMessage = "Value cannot be zero")]
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

        [Required(ErrorMessage = "Please enter a Profit Percentage", AllowEmptyStrings = false)]
        [DisplayName("Profit Percentage")]
        public Decimal Percentage { get; set; }

        public String SellTransactionId { get; set; }
    }
}
