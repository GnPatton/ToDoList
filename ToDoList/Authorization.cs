using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoList.Database;
using ToDoList.Database.dao_sqls;

namespace ToDoList
{
    public partial class Authorization : Form
    {
        Thread th;
        public static User CurrentUser;

        public Authorization()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            User user = UserTable.GetUser(loginField.Text, passwordField.Text);

            if (user.login != null)
            {
                CurrentUser = user;
                this.Close();
                th = new Thread(OpenAccount);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else if (loginField.Text == "" || passwordField.Text == "")
            {
                label3.Text = "Fill all fields!";
                label3.Visible = true;
            }
            else
            {
                label3.Text = "Incorrect login or password";
                label3.Visible = true;
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(OpenRegistration);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void OpenAccount(object obj)
        {
            Application.Run(new Account(CurrentUser));
        }

        private void OpenRegistration(object obj)
        {
            Application.Run(new Registration());
        }

    }
}
