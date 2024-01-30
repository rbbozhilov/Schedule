using Autofac;
using Schedule.Data;
using Schedule.Service.Date;
using Schedule.Service.Employees;
using Schedule.Service.Positions;


namespace Schedule.DI
{
    public static class Container
    {

        private static IContainer _container;

        public static IContainer ContainerDI
        {
            get
            {
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

            _container = builder.Build();
        }
    }
}
