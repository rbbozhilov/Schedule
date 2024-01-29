﻿using Autofac;
using Schedule.DI;
using Schedule.Service.Employees;
using Schedule.Service.Positions;
using Schedule.WPF.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schedule.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ScheduleViewModel viewModel = new ScheduleViewModel();
        private IEmployeeService employeeService = DI.Container.ContainerDI.Resolve<IEmployeeService>();


        public MainWindow()
        {
            InitializeComponent();

            //var positionService = Container.ContainerDI.Resolve<IPositionService>();

            this.DataContext = new ScheduleViewModel();
            PopulateGrid();
            scheduleDataGrid.IsReadOnly = true;
           

        }

        private void PopulateGrid()
        {
            // Add Employees column
            var employeesColumn = new DataGridTextColumn
            {
                Header = "Employees",
                Binding = new System.Windows.Data.Binding("EmployeeName")
            };
            scheduleDataGrid.Columns.Add(employeesColumn);

            // Add columns for dates
            foreach (var date in viewModel.Dates)
            {
                var dateColumn = new DataGridTextColumn
                {
                    Header = date,
                    Binding = new System.Windows.Data.Binding("Shifts[" + viewModel.Dates.IndexOf(date) + "]")
                };
                scheduleDataGrid.Columns.Add(dateColumn);
            }

            // Add rows for each employee
            foreach (var employeeSchedule in viewModel.EmployeeSchedules)
            {
                scheduleDataGrid.Items.Add(employeeSchedule);
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            var isEditted = this.employeeService.EditEmployee(firstName.Text,
                                                              lastName.Text,
                                                              shiftName.Text,
                                                              DateTime.Parse(shiftDate.Text),
                                                              positions.Text);

            if(isEditted == false)
            {
                MessageBox.Show("Something went wrong!");
            }
        }
    }
}
