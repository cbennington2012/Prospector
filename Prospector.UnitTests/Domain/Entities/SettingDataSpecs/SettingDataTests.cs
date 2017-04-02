using NUnit.Framework;
using Prospector.Domain.Entities;

namespace Prospector.UnitTests.Domain.Entities.SettingDataSpecs
{
    public class WhenIGetASettingDataObject : TestBase<SettingData>
    {
        protected override void When()
        {
            base.When();

            Target = new SettingData
            {
                SettingsKey = "Key",
                SettingsValue = "Value"
            };
        }

        [Then]
        public void TheSettingsKeyPropertyIsCorrect()
        {
            Assert.That(Target.SettingsKey, Is.EqualTo("Key"));
        }

        [Then]
        public void TheSettingsValuePropertyIsCorrect()
        {
            Assert.That(Target.SettingsValue, Is.EqualTo("Value"));
        }
    }
}
