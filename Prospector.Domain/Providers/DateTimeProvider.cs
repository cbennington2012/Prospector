﻿using System;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Repositories;

namespace Prospector.Domain.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private readonly ISettingRepository _settingRepository;

        public DateTimeProvider(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public DateTime GetTransactionStartDate(DateTime date)
        {
            return DateTime.Parse(date.ToString("yyyy-MM-01 00:00:00"));
        }

        public DateTime GetTransactionEndDate(DateTime date)
        {
            return DateTime.Parse(DateTime.Parse(date.ToString("yyyy-MM-01 00:00:00")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
        }

        public Decimal GetTotalNumberOfMonths(DateTime startDate, DateTime endDate)
        {
            return Math.Ceiling((endDate.Subtract(startDate).Days/(365.25M/12)));
        }

        public DateTime GetTaxYearStartDate(DateTime startDate)
        {
            var taxYearStartSlug = _settingRepository.GetSettingByKey("TaxYearStart").SettingsValue;

            if (startDate < DateTime.Parse($"{DateTime.Today.Year}-{taxYearStartSlug} 00:00:00"))
            {
                return DateTime.Parse($"{startDate.Year}-{taxYearStartSlug} 00:00:00");
            }

            return DateTime.Parse($"{DateTime.Today.Year}-{taxYearStartSlug} 00:00:00");
        }
    }
}
