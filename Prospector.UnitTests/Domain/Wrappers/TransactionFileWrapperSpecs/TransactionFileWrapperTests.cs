using System;
using System.Collections.Generic;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;
using Prospector.Domain.Wrappers;

namespace Prospector.UnitTests.Domain.Wrappers.TransactionFileWrapperSpecs
{
    public class WhenIGetTheCurrentHoldings : GivenA<TransactionFileWrapper, IList<TransactionData>>
    {
        private const String FilePath = "FilePath";
        private const String FileContents = "FileContents";

        protected override void Given()
        {
            base.Given();

            GetMock<IAppSettingProvider>()
                .Setup(m => m.Get("HoldingsJsonFilePath"))
                .Returns(FilePath);

            GetMock<IIoWrapper>()
                .Setup(m => m.Read(FilePath))
                .Returns(FileContents);
        }

        protected override void When()
        {
            base.When();

            Result = Target.GetCurrentHoldings();
        }

        [Then]
        public void TheAppSettingProviderGetsTheHoldingsJsonFilePathSetting()
        {
            Verify<IAppSettingProvider>(m => m.Get("HoldingsJsonFilePath"));
        }

        [Then]
        public void TheIIoWrpperReadsTheFile()
        {
            Verify<IIoWrapper>(m => m.Read(FilePath));
        }

        [Then]
        public void TheJsonProviderDeserializesTheData()
        {
            Verify<IJsonProvider>(m => m.Deserialize<IList<TransactionData>>(FileContents));
        }
    }

    public class WhenIWriteTheCurrentHoldings : GivenA<TransactionFileWrapper>
    {
        private const String FileContents = "FileContents";
        private const String FilePath = "FilePath";

        private readonly IList<TransactionData> _mockCurrentHoldings = new List<TransactionData>();

        protected override void Given()
        {
            base.Given();

            GetMock<IAppSettingProvider>()
                .Setup(m => m.Get("HoldingsJsonFilePath"))
                .Returns(FilePath);

            GetMock<IJsonProvider>()
                .Setup(m => m.Serialize(_mockCurrentHoldings))
                .Returns(FileContents);
        }

        protected override void When()
        {
            base.When();

            Target.WriteCurrentHoldings(_mockCurrentHoldings);
        }

        [Then]
        public void TheJsonProviderSerializesTheData()
        {
            Verify<IJsonProvider>(m => m.Serialize(_mockCurrentHoldings));
        }

        [Then]
        public void TheAppSettingProviderGetsTheHoldingsJsonFilePathSetting()
        {
            Verify<IAppSettingProvider>(m => m.Get("HoldingsJsonFilePath"));
        }

        [Then]
        public void TheIIoWrpperWritesTheFile()
        {
            Verify<IIoWrapper>(m => m.Write(FilePath, FileContents));
        }
    }
}
