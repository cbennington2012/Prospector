using System;
using System.Linq;
using System.Web.Mvc;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Factories;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;
using Prospector.Domain.Enumerations;
using Prospector.Domain.Parsers;
using Prospector.Presentation.ViewModels;

namespace Prospector.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAutoMapper _autoMapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITransactionFactory _transactionFactory;
        private readonly ISettingRepository _settingRepository;
        private readonly ITransactionFileWrapper _transactionFileWrapper;

        public TransactionsController(ITransactionRepository transactionRepository, IAutoMapper autoMapper, IDateTimeProvider dateTimeProvider, ITransactionFactory transactionFactory, ISettingRepository settingRepository, 
            ITransactionFileWrapper transactionFileWrapper)
        {
            _transactionRepository = transactionRepository;
            _autoMapper = autoMapper;
            _dateTimeProvider = dateTimeProvider;
            _transactionFactory = transactionFactory;
            _settingRepository = settingRepository;
            _transactionFileWrapper = transactionFileWrapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var startDate = _dateTimeProvider.GetTransactionStartDate(DateTime.UtcNow);
            var endDate = _dateTimeProvider.GetTransactionEndDate(DateTime.UtcNow);
            var numberOfMonths = _dateTimeProvider.GetTotalNumberOfMonths(startDate, endDate);

            var data = _transactionRepository.GetTransactions(startDate, endDate);
            var results = data.Select(item => _autoMapper.Map<TransactionData, TransactionViewModel>(item)).ToList();

            var taxYearStartDate = _dateTimeProvider.GetTaxYearStartDate(startDate);
            var taxYearData = _transactionRepository.GetTransactions(taxYearStartDate, endDate);

            var monthlyTargetSetting = _settingRepository.GetSettingByKey("DefaultMonthlyTarget");

            var viewModel = new TransactionSearchViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Results = results,
                MonthlyTarget = Decimal.Parse(monthlyTargetSetting.SettingsValue),
                CumulativeTarget = Decimal.Parse(monthlyTargetSetting.SettingsValue) * numberOfMonths,
                TaxFreeAllowance = Decimal.Parse(_settingRepository.GetSettingByKey("TaxFreeAllowance").SettingsValue),
                TransactionPeriod = _transactionFactory.GetTransactionPeriodValue(data),
                SinceStartTaxYear = _transactionFactory.GetTaxYearValue(taxYearData),
                ShowBuyTransactionsOnly = true
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(TransactionSearchViewModel viewModel)
        {
            var numberOfMonths = _dateTimeProvider.GetTotalNumberOfMonths(viewModel.StartDate, viewModel.EndDate);

            var data = _transactionRepository.GetTransactions(viewModel.StartDate, viewModel.EndDate);
            var results = data.Select(item => _autoMapper.Map<TransactionData, TransactionViewModel>(item)).ToList();

            if (viewModel.ShowBuyTransactionsOnly)
            {
                var soldTransactions = results.Where(x => x.TransactionType == TransactionType.Sell);
                results = results.Where(x => x.TransactionType == TransactionType.Buy && String.IsNullOrEmpty(x.SellTransactionId)).ToList();
                foreach (var item in soldTransactions)
                {
                    results.Remove(results.FirstOrDefault(x => x.Id == item.SellTransactionId));
                }
            }

            var monthlyTargetSetting = _settingRepository.GetSettingByKey("DefaultMonthlyTarget");

            viewModel.Results = results;
            viewModel.MonthlyTarget = Decimal.Parse(monthlyTargetSetting.SettingsValue);
            viewModel.CumulativeTarget = Decimal.Parse(monthlyTargetSetting.SettingsValue) *numberOfMonths;
            viewModel.TransactionPeriod = _transactionFactory.GetTransactionPeriodValue(data);
            viewModel.SinceStartTaxYear = 0;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Add(TransactionViewModel viewModel)
        {
            viewModel.Percentage = 2.0M;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(TransactionViewModel viewModel, String isPost)
        {
            var data = _autoMapper.Map<TransactionViewModel, TransactionData>(viewModel);

            _transactionRepository.AddTransaction(data);
            _transactionFileWrapper.WriteCurrentHoldings(_transactionRepository.GetCurrentHoldings());

            var viewResult = new ViewResult
            {
                ViewName = "Add"
            };

            viewResult.ViewBag.Message =
                $"{EnumParser.GetDescription(viewModel.TransactionType)} transaction for {viewModel.Code} completed";

            return viewResult;
        }

        [HttpGet]
        public ActionResult Sell(String Id)
        {
            var data = _transactionRepository.GetTransactionById(Id);
            var viewModel = _autoMapper.Map<TransactionData, TransactionViewModel>(data);
            viewModel.TransactionType = TransactionType.Sell;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Sell(TransactionViewModel viewModel)
        {
            var data = _autoMapper.Map<TransactionViewModel, TransactionData>(viewModel);
            data.SellTransactionId = viewModel.Id;
            data.Tax = 0;
            data.Percentage = 0;

            _transactionRepository.AddTransaction(data);
            _transactionFileWrapper.WriteCurrentHoldings(_transactionRepository.GetCurrentHoldings());

            var viewResult = new ViewResult
            {
                ViewName = "Sell"
            };

            viewResult.ViewBag.Message =
                $"{EnumParser.GetDescription(viewModel.TransactionType)} transaction for {viewModel.Code} completed";

            return viewResult;
        }
    }
}