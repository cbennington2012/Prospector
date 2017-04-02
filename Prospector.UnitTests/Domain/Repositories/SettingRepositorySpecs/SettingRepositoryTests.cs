using System;
using System.Collections.Generic;
using System.Data;
using Moq;
using NUnit.Framework;
using Prospector.Domain.Contracts.Parsers;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Entities;
using Prospector.Domain.Repositories;

namespace Prospector.UnitTests.Domain.Repositories.SettingRepositorySpecs
{
    public class WhenIGetTheSettings : GivenA<SettingRepository>
    {
        private DataTable _sqlData;

        protected override void Given()
        {
            base.Given();

            _sqlData = new DataTable();
            _sqlData.Columns.Add(new DataColumn("Test"));
            _sqlData.Rows.Add(new [] {101});

            GetMock<IMySqlProvider>()
                .Setup(m => m.GetData("Prospector", "spGetSettings", It.IsAny<IDictionary<String, Object>>()))
                .Returns(_sqlData);
        }

        protected override void When()
        {
            base.When();

            Target.Get();
        }

        [Then]
        public void TheMySqlDataProviderGetsTheData()
        {
            Verify<IMySqlProvider>(m => m.GetData("Prospector", "spGetSettings", It.IsAny<IDictionary<String, Object>>()));
        }

        [Then]
        public void TheDataObjectParserGetsTheObjectFromTheData()
        {
            Verify<IDataObjectParser>(m => m.GetObject<SettingData>(It.IsAny<DataRow>()));
        }
    }

    public class WhenIGetTheSettingByKey : GivenA<SettingRepository>
    {
        private DataTable _sqlData;

        protected override void Given()
        {
            base.Given();

            _sqlData = new DataTable();
            _sqlData.Columns.Add(new DataColumn("Test"));
            _sqlData.Rows.Add(new[] { 101 });

            GetMock<IMySqlProvider>()
                .Setup(m => m.GetData("Prospector", "spGetSettingByKey", It.IsAny<IDictionary<String, Object>>()))
                .Returns(_sqlData);
        }

        protected override void When()
        {
            base.When();

            Target.GetSettingByKey("Key");
        }

        [Then]
        public void TheMySqlDataProviderGetsTheData()
        {
            Verify<IMySqlProvider>(m => m.GetData("Prospector", "spGetSettingByKey", It.IsAny<IDictionary<String, Object>>()));
        }

        [Then]
        public void TheDataObjectParserGetsTheObjectFromTheData()
        {
            Verify<IDataObjectParser>(m => m.GetObject<SettingData>(It.IsAny<DataRow>()));
        }
    }

    public class WhenIGetTheGetSettingByKeyParameters : GivenA<SettingRepository, IDictionary<String, Object>>
    {
        protected override void When()
        {
            base.When();

            Result = Target.GetSettingByKeyParameters("Key");
        }

        [Then]
        public void TheSettingKeyParameterHasBeenSet()
        {
            Assert.That(Result["@SettingKey"], Is.EqualTo("Key"));
        }
    }
}
