using System;
using System.Linq;
using System.Web.Mvc;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Domain.Parsers;
using Prospector.Presentation.ViewModels;

namespace Prospector.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAutoMapper _autoMapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IAppSettingProvider _appSettingProvider;

        public TransactionsController(ITransactionRepository transactionRepository, IAutoMapper autoMapper, IDateTimeProvider dateTimeProvider, IAppSettingProvider appSettingProvider)
        {
            _transactionRepository = transactionRepository;
            _autoMapper = autoMapper;
            _dateTimeProvider = dateTimeProvider;
            _appSettingProvider = appSettingProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var startDate = _dateTimeProvider.GetTransactionStartDate(DateTime.UtcNow);
            var endDate = _dateTimeProvider.GetTransactionEndDate(DateTime.UtcNow);

            var data = _transactionRepository.GetTransactions(startDate, endDate);
            var results = data.Select(item => _autoMapper.Map<TransactionData, TransactionViewModel>(item)).ToList();

            var viewModel = new TransactionSearchViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Results = results,
                MonthlyTarget = Decimal.Parse(_appSettingProvider.Get("MonthlyTarget")),
                TaxFreeAllowance = Decimal.Parse(_appSettingProvider.Get("TaxFreeAllowance"))
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(TransactionSearchViewModel viewModel)
        {
            var data = _transactionRepository.GetTransactions(viewModel.StartDate, viewModel.EndDate);
            var results = data.Select(item => _autoMapper.Map<TransactionData, TransactionViewModel>(item)).ToList();

            viewModel.Results = results;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Add(TransactionViewModel viewModel)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(TransactionViewModel viewModel, String isPost)
        {
            var data = _autoMapper.Map<TransactionViewModel, TransactionData>(viewModel);

            _transactionRepository.AddTransaction(data);

            var viewResult = new ViewResult
            {
                ViewName = "Index"
            };

            viewResult.ViewBag.Message =
                $"{EnumParser.GetDescription(viewModel.TransactionType)} transaction for {viewModel.Code} completed";

            return viewResult;
        }
    }
}