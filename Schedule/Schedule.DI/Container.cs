using Autofac;
using Schedule.Data;
using Schedule.Service.Date;
using Schedule.Service.Employees;
using Schedule.Service.PasswordHash;
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

            builder.RegisterType<ScheduleDbContext>().AsSelf();
            builder.RegisterType<PositionService>().As<IPositionService>();
            builder.RegisterType<DateService>().As<IDateService>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<Hasher>().As<IHasher>();

            _container = builder.Build();
        }
    }
}
