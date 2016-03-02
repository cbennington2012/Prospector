using NUnit.Framework;

namespace Prospector.UnitTests
{
    public abstract class TestBase<T>
    {
        public T Target { get; set; }

        [SetUp]
        public void Setup()
        {
            Given();
            When();
        }

        protected virtual void Given()
        {
        }

        protected virtual void When()
        {
        }
    }

    public class ThenAttribute : TestAttribute
    { }
}
