using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Schedule.WPF.Login
{
    public class LoginViewModel
    {

        public bool IsLoggedIn { get; set; }


        public string Username { get; set; }


        public string Password { get; set; }


        private void Login()
        {
            // Perform authentication logic here
            // For simplicity, let's assume the login is successful
            IsLoggedIn = true;
        }

    }
}
