using System;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Utils.Helpers;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class PropertyReflectionTest
    {
        [Test]
        public void ShouldReturnTrueIfPropertyExists()
        {
            var dummyObject = new DummyObject();
            var propertyReflection = new PropertyReflection();
            var result = propertyReflection.HasProperty(dummyObject, "DateTime");

            Assert.AreEqual(true, result);
        }

        [Test]
        public void ShouldReturnFalseIfPropertyNotExists()
        {
            var dummyObject = new DummyObject();
            var propertyReflection = new PropertyReflection();
            var result = propertyReflection.HasProperty(dummyObject, "FooBar");

            Assert.AreEqual(false, result);
        }

        [Test]
        public void ShouldGetPropertyValueSuccessfully()
        {
            var dummyObject = new DummyObject { Name = "Stars" };
            var propertyReflection = new PropertyReflection();
            var result = propertyReflection.GetPropertyValue(dummyObject, "Name");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "Stars");
        }

        [Test]
        public void ShouldThrowExceptionWhenPropertyNotExistingOnGettingValue()
        {
            var dummyObject = new DummyObject();
            var propertyReflection = new PropertyReflection();
            var result = Assert.Throws<Exception>(() =>
                propertyReflection.GetPropertyValue(dummyObject, "Doughnut"));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Exception), result);
            Assert.AreEqual("Property Doughnut does not exist on DummyObject object", result.Message);
        }

        [Test]
        public void ShouldGetPropertyValueSuccessfullyByRecursion()
        {
            var dummyObject = new DummyObjectComplex { Dummy = new DummyObject { Name = "Stars" } };
            var propertyReflection = new PropertyReflection();
            var result = propertyReflection.GetPropertyValue(dummyObject, "Name", true);

            Assert.IsNotNull(result);
            Assert.AreEqual("Stars", result.ToString());
        }
        
        [Test]
        public void ShouldGetPropertyValueSuccessfullyByRecursionOnParentObject()
        {
            var dummyObject = new DummyObject { Id = 5 };
            var propertyReflection = new PropertyReflection();
            var result = propertyReflection.GetPropertyValue(dummyObject, "Id", true);

            Assert.IsNotNull(result);
            Assert.AreEqual(5, Convert.ToInt32(result));
        }

        [Test]
        public void ShouldReturnNullWhenGetPropertyValueByRecursionFoundNoProperty()
        {
            var dummyObject = new EmptyDummy();
            var propertyReflection = new PropertyReflection();
            var result = propertyReflection.GetPropertyValue(dummyObject, "Doughnut", true);

            Assert.IsNull(result);
        }

        [Test]
        public void ShouldReturnNullWhenGetPropertyValueByRecursionCannotFindProperty()
        {
            var dummyObject = new EmptyDummy();
            var propertyReflection = new PropertyReflection();
            var result = propertyReflection.GetPropertyValue(dummyObject, "Doughnut", true);

            Assert.IsNull(result);
        }

        [Test]
        public void ShouldSetPropertySuccessfully()
        {
            var dummyObject = new DummyObject();
            var propertyReflection = new PropertyReflection();

            Assert.DoesNotThrow(() => propertyReflection.SetProperty(dummyObject, "Name", "FooBar"));
            Assert.AreEqual(dummyObject.Name, "FooBar");
        }

        [Test]
        public void ShouldThrowExceptionWhenPropertyNotExistingOnSettingValue()
        {
            var dummyObject = new DummyObject();
            var propertyReflection = new PropertyReflection();
            var result = Assert.Throws<Exception>(() =>
                propertyReflection.SetProperty(dummyObject, "Lighter", "FooBar"));

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Exception), result);
            Assert.AreEqual("Property Lighter does not exist on DummyObject object", result.Message);
        }

        protected class DummyObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime DateTime { get; set; }
        }

        protected class DummyObjectComplex
        {
            public int SomeId { get; set; }
            public DummyObject Dummy { get; set; }
        }

        protected class EmptyDummy {}
    }
}
