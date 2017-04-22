using System;
using NUnit.Framework;
using Prospector.Presentation.Formatters;

namespace Prospector.UnitTests.Presentation.Formatters.SplitStringFormatterSpecs
{
    public class WhenIConvertAString : TestBase<Object>
    {
        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(SplitStringFormatter.Convert("ThisIsTheInputString"), Is.EqualTo("This Is The Input String"));
        }
    }
}
