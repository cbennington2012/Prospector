using System;
using System.Data;
using NUnit.Framework;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Parsers;

namespace Prospector.UnitTests.Domain.Parsers.DataObjectParserSpecs
{
    public class WhenIGetAnObjectFromADataRow : GivenA<DataObjectParser>
    {
        private TestObject _result;
        private DataTable _dataTable;

        protected override void Given()
        {
            base.Given();

            _dataTable = new DataTable();
            _dataTable.Columns.Add(new DataColumn("ValueOne", typeof(int)));
            _dataTable.Columns.Add(new DataColumn("ValueTwo", typeof(string)));
            _dataTable.Columns.Add(new DataColumn("ValueThree", typeof(bool)));
            _dataTable.Columns.Add(new DataColumn("ValueFour", typeof(string)));
            _dataTable.Columns.Add(new DataColumn("ValueFive", typeof(string)));
            _dataTable.Columns.Add(new DataColumn("ValueSix", typeof(int)));
            _dataTable.Columns.Add(new DataColumn("ValueSeven", typeof(int)));
            _dataTable.Rows.Add(123, "Test String\0", true, "ValueTwo", "ThisShouldNotWork", DBNull.Value, DBNull.Value);

            GetMock<IStructureMapWrapper>().Setup(m => m.GetInstance(typeof(TestObject))).Returns(new TestObject());
        }

        protected override void When()
        {
            base.When();

            _result = Target.GetObject<TestObject>(_dataTable.Rows[0]);
        }

        [Then]
        public void TheStructureMapWrapperGetsTheInstance()
        {
            Verify<IStructureMapWrapper>(m => m.GetInstance(typeof(TestObject)));
        }

        [Then]
        public void ThePropertyValueOneHasBeenSet()
        {
            Assert.That(_result.ValueOne, Is.EqualTo(123));
        }

        [Then]
        public void ThePropertyValuetwoHasBeenSet()
        {
            Assert.That(_result.ValueTwo, Is.EqualTo("Test String"));
        }

        [Then]
        public void ThePropertyValueThreeHasBeenSet()
        {
            Assert.IsTrue(_result.ValueThree);
        }

        [Then]
        public void ThePropertyValueFourHasBeenSet()
        {
            Assert.That(_result.ValueFour, Is.EqualTo(TestEnum.ValueTwo));
        }

        [Then]
        public void ThePropertyValueFiveIsNull()
        {
            Assert.That(_result.ValueFive, Is.Null);
        }

        [Then]
        public void ThePropertyValueSixIsTheTypeDefault()
        {
            Assert.That(_result.ValueSix, Is.EqualTo(0));
        }

        [Then]
        public void ThePropertyValueSevenIsTheTypeDefault()
        {
            Assert.That(_result.ValueSeven, Is.Null);
        }
    }

    internal class TestObject
    {
        public int ValueOne { get; set; }
        public string ValueTwo { get; set; }
        public bool ValueThree { get; set; }
        public TestEnum ValueFour { get; set; }

        [Prospector.Domain.Attributes.Ignore]
        public string ValueFive { get; set; }

        public int ValueSix { get; set; }
        public string ValueSeven { get; set; }
    }

    internal enum TestEnum
    {
        ValueOne,
        ValueTwo,
        ValueThree
    }
}