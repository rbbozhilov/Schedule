using Autofac;
using Schedule.Data;
using Schedule.Service.Positions;
using System.Reflection;


namespace Schedule.DI
{
    public static class Container
    {

        private static IContainer _container;

        public static IContainer ContainerDI
        {
            get
            {
                // If the container is null, initialize it.
                if (_container == null)
                {
                    Init();
                }
                return _container;
            }
        }

        private static void Init()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PositionService>().As<IPositionService>();

            _container = builder.Build();
        }
    }
}
