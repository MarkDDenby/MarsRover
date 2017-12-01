using System;
using NUnit.Framework;
using Castle.Windsor;
using Denby.Contracts;


namespace Denby.MarsRover.Core.Tests.Unit
{
    [TestFixture]
    public class WindsorContainerFixture
    {
        private IWindsorContainer _sut = WindsorContainerFactory.Create(5);

        [Test]
        public void CanResolveMarsController()
        {
            var marsController = _sut.Resolve<MarsRoverController>();
            Assert.IsNotNull(marsController);
        }
        
        [Test]
        public void CanResolveMarsPlanet()
        {
            var planet = _sut.Resolve<IPlanet>();
            Assert.IsNotNull(planet);
        }

        [Test]
        public void CanResolveCompass()
        {
            var compass = _sut.Resolve<ICompass>();
            Assert.IsNotNull(compass);
        }

        [Test]
        public void CanResolveCoordinate()
        {
            var coordinate = _sut.Resolve<ICoordinates>();
            Assert.IsNotNull(coordinate);
        }

        [Test]
        public void CanResolveNavigation()
        {
            var navigation = _sut.Resolve<INavigation>();
            Assert.IsNotNull(navigation);
        }

        [Test]
        public void CanResolveRover()
        {
            var rover = _sut.Resolve<IRover>();
            Assert.IsNotNull(rover);
        }
    }
}
