using Autofac;

using Schedule.Service.Employees;

using Schedule.WPF.Schedules;
using System;

using System.Windows;
using System.Windows.Controls;


namespace Schedule.WPF
{

    public partial class MainWindow
    {
        private ScheduleViewModel viewModel = new ScheduleViewModel();
        private IEmployeeService employeeService = DI.Container.ContainerDI.Resolve<IEmployeeService>();


        public MainWindow()
        {
            InitializeComponent();


            this.DataContext = new ScheduleViewModel();
            PopulateGrid();
            scheduleDataGrid.IsReadOnly = true;


        }

        private void PopulateGrid()
        {

            var employeesColumn = new DataGridTextColumn
            {
                Header = "Employees",
                Binding = new System.Windows.Data.Binding("EmployeeName")
            };
            scheduleDataGrid.Columns.Add(employeesColumn);


            foreach (var date in viewModel.Dates)
            {
                var dateColumn = new DataGridTextColumn
                {
                    Header = date,
                    Binding = new System.Windows.Data.Binding("Shifts[" + viewModel.Dates.IndexOf(date) + "]")
                };
                scheduleDataGrid.Columns.Add(dateColumn);
            }


            foreach (var employeeSchedule in viewModel.EmployeeSchedules)
            {
                scheduleDataGrid.Items.Add(employeeSchedule);
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            var isEditted = this.employeeService.EditEmployee(firstNameEdit.Text,
                                                              lastNameEdit.Text,
                                                              shiftNameEdit.Text,
                                                              DateTime.Parse(shiftDateEdit.Text),
                                                              positionsEdit.Text);

            if (isEditted == false)
            {
                MessageBox.Show("Something went wrong!");
                return;
            }

            MessageBox.Show("Succesfull editted!");
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            this.employeeService.AddEmployee(firstNameAdd.Text,
                                             lastNameAdd.Text,
                                             shiftNameAdd.Text,
                                             DateTime.Parse(shiftDateAdd.Text),
                                             positionsAdd.Text);

            MessageBox.Show("Succesfull added!");
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            string working = "Working days: ";

            days.Content = working + this.employeeService.DaysOfWork(firstName.Text,
                                                                     lastName.Text,
                                                                     DateTime.Parse(startDate.Text),
                                                                     DateTime.Parse(endDate.Text));
        }
    }
}
