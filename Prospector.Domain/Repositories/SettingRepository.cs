using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Prospector.Domain.Contracts.Parsers;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly IMySqlProvider _mySqlProvider;
        private readonly IDataObjectParser _dataObjectParser;

        public SettingRepository(IMySqlProvider mySqlProvider, IDataObjectParser dataObjectParser)
        {
            _mySqlProvider = mySqlProvider;
            _dataObjectParser = dataObjectParser;
        }

        public IList<SettingData> Get()
        {
            var results = _mySqlProvider.GetData(Constants.ConnectionStrings.Prospector, "spGetSettings", new Dictionary<string, object>());

            return (from DataRow item in results.Rows select _dataObjectParser.GetObject<SettingData>(item)).ToList();
        }

        public SettingData GetSettingByKey(string key)
        {
            var results = _mySqlProvider.GetData(Constants.ConnectionStrings.Prospector, "spGetSettingByKey", GetSettingByKeyParameters(key));

            return (from DataRow item in results.Rows select _dataObjectParser.GetObject<SettingData>(item)).FirstOrDefault();
        }

        internal IDictionary<String, Object> GetSettingByKeyParameters(String key)
        {
            return new Dictionary<string, object>
            {
                { "@SettingKey", key }
            };
        } 
    }
}
