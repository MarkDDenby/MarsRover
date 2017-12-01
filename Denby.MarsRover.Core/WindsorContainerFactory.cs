using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Denby.Common;
using Denby.Contracts;

namespace Denby.MarsRover.Core
{
    public static class WindsorContainerFactory
    {
        // we would pass in a configuration class that contains the maximumNumberOfCommands and other config settings

        public static IWindsorContainer Create(int maximumNumberOfCommands)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());
            container.Register(Component.For<IPlanet>().ImplementedBy<Mars>().LifestyleTransient());
            container.Register(Component.For<ICompass>().ImplementedBy<Compass>().LifestyleTransient());
            container.Register(Component.For<ICoordinates>().ImplementedBy<Coordinates>().LifestyleTransient());
            container.Register(Component.For<INavigation>().ImplementedBy<Navigation>().LifestyleTransient()
                     .DependsOn(Dependency.OnValue<CardinalHeading>(CardinalHeading.South)));
            container.Register(Component.For<IRover>().ImplementedBy<Core.MarsRover>().LifestyleTransient());
            container.Register(Component.For<MarsRoverController>().ImplementedBy<Core.MarsRoverController>().LifestyleTransient()
                     .DependsOn(Dependency.OnValue<int>(maximumNumberOfCommands)));

            return container;
        }
    }
}
