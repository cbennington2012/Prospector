using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;
using Prospector.Domain.Providers;

namespace Prospector.UnitTests.Domain.Providers.HoldingDataProviderSpecs
{
    public abstract class GivenAHoldingDataProvider : GivenA<HoldingDataProvider>
    {
        private const String HoldingJsonFilePath = "HoldingsJsonFilePath\\";
        protected const String FileName = "HoldingsJsonFilePath\\Holdings.json";
        protected const String FileContents = "FileContents";

        protected override void Given()
        {
            base.Given();

            GetMock<IAppSettingProvider>()
                .Setup(m => m.Get("HoldingsJsonFilePath"))
                .Returns(HoldingJsonFilePath);

            GetMock<IIoWrapper>()
                .Setup(m => m.Read(FileName))
                .Returns(FileContents);
        }

        [Then]
        public void TheAppSettingProviderGetsTheKeysValue()
        {
            Verify<IAppSettingProvider>(m => m.Get("HoldingsJsonFilePath"));
        }
    }

    public class WhenIGetTheHoldingData : GivenAHoldingDataProvider
    {
        protected override void When()
        {
            base.When();

            Target.Get();
        }

        [Then]
        public void TheIoWrapperReadsTheFile()
        {
            Verify<IIoWrapper>(m => m.Read(FileName));
        }

        [Then]
        public void TheJsonProviderDeserializesTheFileContents()
        {
            Verify<IJsonProvider>(m => m.Deserialize<IList<HoldingData>>(FileContents));
        }
    }

    public class WhenISaveTheHoldingsData : GivenAHoldingDataProvider
    {
        private readonly IList<HoldingData> _mockData = new List<HoldingData>();

        protected override void Given()
        {
            base.Given();

            GetMock<IJsonProvider>()
                .Setup(m => m.Serialize(_mockData))
                .Returns(FileContents);
        }

        protected override void When()
        {
            base.When();

            Target.Save(_mockData);
        }

        [Then]
        public void TheJsonProviderSerializesTheData()
        {
            Verify<IJsonProvider>(m => m.Serialize(_mockData));
        }

        [Then]
        public void TheIoWrapperWritesTheData()
        {
            Verify<IIoWrapper>(m => m.Write(FileName, FileContents));
        }
    }
}
