﻿using Autofac;
using Schedule.Service.Date;
using Schedule.Service.Employees;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
namespace Schedule.WPF.Schedules
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _dates;

        private IDateService dateService = DI.Container.ContainerDI.Resolve<IDateService>();
        private IEmployeeService employeeService = DI.Container.ContainerDI.Resolve<IEmployeeService>();

        public ScheduleViewModel()
        {

            var employees = this.employeeService.GetAllEmployees();

            Dates = new ObservableCollection<string>(this.dateService.GetDaysFromMonth());


            EmployeeSchedules = new ObservableCollection<EmployeeSchedule>();

            foreach (var employee in employees)
            {
                var fullName = employee.Split(' ').ToArray();
                var firstName = fullName[0];
                var lastName = fullName[1];

                var shiftsOfEmployee = this.employeeService.ShiftOfEmployeeForMonth(firstName, lastName);


                EmployeeSchedules.Add(new EmployeeSchedule
                {
                    EmployeeName = employee,
                    Shifts = new ObservableCollection<string>(shiftsOfEmployee)
                });

            }


        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Dates
        {
            get { return _dates; }
            set
            {
                _dates = value;
                OnPropertyChanged(nameof(Dates));
            }
        }

        private ObservableCollection<EmployeeSchedule> _employeeSchedules;
        public ObservableCollection<EmployeeSchedule> EmployeeSchedules
        {
            get { return _employeeSchedules; }
            set
            {
                _employeeSchedules = value;
                OnPropertyChanged(nameof(EmployeeSchedules));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class EmployeeSchedule
    {
        public string EmployeeName { get; set; }
        public ObservableCollection<string> Shifts { get; set; }
    }
}
