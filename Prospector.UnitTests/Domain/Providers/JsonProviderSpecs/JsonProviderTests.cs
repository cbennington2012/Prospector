using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prospector.Domain.Providers;

namespace Prospector.UnitTests.Domain.Providers.JsonProviderSpecs
{
    public class WhenISerializeAnObject : GivenA<JsonProvider, String>
    {
        protected override void When()
        {
            base.When();

            Result = Target.Serialize(new TestObject
            {
                ValueOne = "Testing",
                ValueTwo = 100,
                ValueThree = true
            });
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo("{\"ValueOne\":\"Testing\",\"ValueTwo\":100,\"ValueThree\":true}"));
        }
    }

    public class WhenIDesserializeAnObject : GivenA<JsonProvider, TestObject>
    {
        protected override void When()
        {
            base.When();

            Result = Target.Deserialize<TestObject>("{\"ValueOne\":\"Test Value\",\"ValueTwo\":500,\"ValueThree\":true}");
        }

        [Then]
        public void TheValueOnePropertyIsCorrect()
        {
            Assert.That(Result.ValueOne, Is.EqualTo("Test Value"));
        }

        [Then]
        public void TheValueTwoPropertyIsCorrect()
        {
            Assert.That(Result.ValueTwo, Is.EqualTo(500));
        }

        [Then]
        public void TheValueThreePropertyIsCorrect()
        {
            Assert.IsTrue(Result.ValueThree);
        }
    }

    public class TestObject
    {
        public String ValueOne { get; set; }
        public int ValueTwo { get; set; }
        public Boolean ValueThree { get; set; }
    }
}
