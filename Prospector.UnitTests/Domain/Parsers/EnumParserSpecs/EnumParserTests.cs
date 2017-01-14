using NUnit.Framework;
using Prospector.Domain.Parsers;

namespace Prospector.UnitTests.Domain.Parsers.EnumParserSpecs
{
    public abstract class GivenAnEnumParser
    {
    }

    public class WhenIGetAnEnumsDescription : GivenAnEnumParser
    {
        [Then]
        public void TheEnumsDescriptionIsItsTextDescription()
        {
            Assert.That(EnumParser.GetDescription(TestEnumWithDescriptions.ValueTwo), Is.EqualTo("Value Two"));
        }
    }

    public class WhenIGetAnEnumsDescriptionWithNoDescriptionAttribute : GivenAnEnumParser
    {
        [Then]
        public void TheEnumsDescriptionIsTheEnum()
        {
            Assert.That(EnumParser.GetDescription(TestEnum.ValueTwo), Is.EqualTo("ValueTwo"));
        }
    }

    public class WhenIGetAnEnumByItsValue : GivenAnEnumParser
    {
        [Then]
        public void TheEnumReturnedIsCorrect()
        {
            Assert.That(EnumParser.GetEnumFromValue("ValueTwo", typeof(TestEnum)), Is.EqualTo(TestEnum.ValueTwo));
        }
    }

    internal enum TestEnum
    {
        ValueZero = 0,
        ValueOne = 1,
        ValueTwo = 10,
        ValueThree = 100
    }

    internal enum TestEnumWithDescriptions
    {
        [System.ComponentModel.Description("Value One")]
        ValueOne = 1,
        [System.ComponentModel.Description("Value Two")]
        ValueTwo = 10,
        [System.ComponentModel.Description("Value Three")]
        ValueThree = 100
    }
}