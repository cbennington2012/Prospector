using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prospector.Presentation.ViewModels
{
    public class TransactionSearchViewModel
    {
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public IList<TransactionViewModel> Results { get; set; }

        public Decimal MonthlyTarget { get; set; }
        public Decimal TaxFreeAllowance { get; set; }
        public Decimal TransactionPeriod { get; set; }
        public Decimal SinceStartTaxYear { get; set; }
    }
}
