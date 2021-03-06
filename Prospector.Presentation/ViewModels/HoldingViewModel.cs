﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prospector.Presentation.ViewModels
{
    public class HoldingViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter the Code", AllowEmptyStrings = false)]
        public String Code { get; set; }

        [Required(ErrorMessage = "Please enter the date", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter the amount of shares")]
        [Range(1, 100000, ErrorMessage = "Value cannot be zero")]
        public int Shares { get; set; }

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
        public Decimal Cost { get; set; }
        public Decimal BreakEvenPrice { get; set; }
        public Decimal Percentage { get; set; }
        public Decimal ProfitPrice { get; set; }
        public Decimal Earnings { get; set; }
    }
}
