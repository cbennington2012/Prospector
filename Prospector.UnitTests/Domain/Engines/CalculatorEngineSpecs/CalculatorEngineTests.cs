using System;
using NUnit.Framework;
using Prospector.Domain.Engines;

namespace Prospector.UnitTests.Domain.Engines.CalculatorEngineSpecs
{
    public class WhenICalculateTheCost : GivenA<CalculatorEngine, Decimal>
    {
        protected override void When()
        {
            base.When();

            Result = Target.CalculateCost(1000, 100, 5.95M, 25, 1);
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo(1031.95));
        }
    }

    public class WhenICalculateTheBreakEvenPrice : GivenA<CalculatorEngine, Decimal>
    {
        protected override void When()
        {
            base.When();

            Result = Target.CalculateBreakEvenPrice(1000, 100, 5.95M, 25, 1);
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo(103.89));
        }
    }

    public class WhenICalculateTheProfitPrice : GivenA<CalculatorEngine, Decimal>
    {
        protected override void When()
        {
            base.When();

            Result = Target.CalculateProfitPrice(1000, 100, 5.95M, 25, 1, 1.1M);
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo(113.89));
        }
    }

    public class WhenICalculateTheEarnings : GivenA<CalculatorEngine, Decimal>
    {
        protected override void When()
        {
            base.When();

            Result = Target.CalculateEarnings(1000, 113, 5.95M, 1030, 1);
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo(93.05));
        }
    }
}
