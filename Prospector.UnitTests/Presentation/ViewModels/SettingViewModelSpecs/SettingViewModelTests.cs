using NUnit.Framework;
using Prospector.Presentation.ViewModels;

namespace Prospector.UnitTests.Presentation.ViewModels.SettingViewModelSpecs
{
    public class WhenIGetASettingViewModel : TestBase<SettingViewModel>
    {
        protected override void When()
        {
            base.When();

            Target = new SettingViewModel
            {
                Key = "Key",
                Value = "Value"
            };
        }

        [Then]
        public void TheKeyPropertyIsCorrect()
        {
            Assert.That(Target.Key, Is.EqualTo("Key"));
        }

        [Then]
        public void TheValuePropertyIsCorrect()
        {
            Assert.That(Target.Value, Is.EqualTo("Value"));
        }
    }
}
