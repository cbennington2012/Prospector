using System;
using System.Web.Mvc;
using Prospector.Domain.Contracts.AutoMapping;
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

        public TransactionsController(ITransactionRepository transactionRepository, IAutoMapper autoMapper)
        {
            _transactionRepository = transactionRepository;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
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

            _transactionRepository.Add(data);

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