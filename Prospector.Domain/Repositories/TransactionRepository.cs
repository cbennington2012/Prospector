using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Prospector.Domain.Contracts.Parsers;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Domain.Parsers;

namespace Prospector.Domain.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private const String ConnectionString = "Prospector";

        private readonly IMySqlProvider _mySqlProvider;
        private readonly IDataObjectParser _dataObjectParser;

        public TransactionRepository(IMySqlProvider mySqlProvider, IDataObjectParser dataObjectParser)
        {
            _mySqlProvider = mySqlProvider;
            _dataObjectParser = dataObjectParser;
        }

        public IList<TransactionData> GetCurrentHoldings()
        {
            var results = _mySqlProvider.GetData(ConnectionString, "spGetCurrentHoldings", new Dictionary<string, object>());

            return (from DataRow item in results.Rows select _dataObjectParser.GetObject<TransactionData>(item)).ToList();
        }

        public void AddTransaction(TransactionData data)
        {
            _mySqlProvider.ExecuteProcedure(ConnectionString, "spInsertTransaction", GetInsertParameters(data));
        }

        public IList<TransactionData> GetTransactions(DateTime startDate, DateTime endDate)
        {
            var results = _mySqlProvider.GetData(ConnectionString, "spGetTransactions", GetSearchParameters(startDate, endDate));

            return (from DataRow item in results.Rows select _dataObjectParser.GetObject<TransactionData>(item)).ToList();
        }

        internal IDictionary<String, Object> GetInsertParameters(TransactionData transactionData)
        {
            return new Dictionary<string, object>
            {
                { "@Id", transactionData.Id },
                { "@TransactionType", EnumParser.GetDescription(transactionData.TransactionType) },
                { "@Code", transactionData.Code },
                { "@Date", transactionData.Date.ToString("yyyy-MM-dd HH:mm:ss") },
                { "@Shares", transactionData.Shares },
                { "@Price", transactionData.Price },
                { "@Tax", transactionData.Tax },
                { "@Commission", transactionData.Commission },
                { "@Levy", transactionData.Levy },
                { "@Percentage", transactionData.Percentage }
            };
        }

        internal IDictionary<String, Object> GetSearchParameters(DateTime startDate, DateTime endDate)
        {
            return new Dictionary<string, object>
            {
                { "@StartDate", startDate.ToString("yyyy-MM-dd 00:00:00") },
                { "@EndDate", endDate.ToString("yyyy-MM-dd 23:59:59")}
            };
        }
    }
}
