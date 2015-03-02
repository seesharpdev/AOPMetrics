using NUnit.Framework;
using StructureMap;

using AOPMetrics.Interceptors.Registries;

namespace AOPMetrics.Interceptors.UnitTests
{
    [TestFixture]
    public class InterceptionRegistryTests
    {
        [Test]
        //[Ignore]
        public void ShouldEnrichWithDecorator()
        {
            // Arrange
            var container = new Container(new InterceptionRegistry());

            // Act
            var instance = container.GetInstance<IDocumentService>();

            // Assert
            Assert.AreEqual("Castle.Proxies.IDocumentServiceProxy", instance.GetType().ToString());
        }

        [Test]
        public void ShouldInvokeDecoratorBeforeTheConcreteImplementation()
        {
            // Arrange
            var container = new Container(new InterceptionRegistry());
            var instance = container.GetInstance<IDocumentService>();

            // Act
            var response = instance.Start("FirstArgument", "SecondArgument");

            // Assert
            Assert.IsNotNullOrEmpty(response);
        }
    }
}
