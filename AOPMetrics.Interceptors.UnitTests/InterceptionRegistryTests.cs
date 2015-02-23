﻿using NUnit.Framework;
using StructureMap;

using AOPMetrics.Interceptors.Registries;

namespace AOPMetrics.Interceptors.UnitTests
{
    [TestFixture]
    public class InterceptionRegistryTests
    {
        [Test]
        public void ShouldEnrichWithDecorator()
        {
            // Arrange
            var container = new Container(new InterceptionRegistry());

            // Act
            var instance = container.GetInstance<IDocumentService>();

            // Assert
            Assert.AreEqual(typeof(DocumentServiceMetricsAdapter), instance.GetType());
            //Assert.IsAssignableFrom<ClassThatNeedsSomeBootstrapping>(instance.GetType());
        }

        [Test]
        public void ShouldInvokeDecoratorBeforeTheConcreteImplementation()
        {
            // Arrange
            var container = new Container(new InterceptionRegistry());
            var instance = container.GetInstance<IDocumentService>();

            // Act
            instance.Start();

            // Assert
        }
    }
}