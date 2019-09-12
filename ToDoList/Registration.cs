using System;
using System.Windows.Forms;
using ToDoList.Database.dao_sqls;
using ToDoList.Database;
using System.Threading;

namespace ToDoList
{
    public partial class Registration : Form
    {
        Thread th;

        public Registration()
        {
            InitializeComponent();
        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            if (regPassField.Text == "" || regPassRepField.Text == "" || regLoginField.Text == "")
            {
                label5.Text = "Fill all fields!";
                label5.Visible = true; 
            }
            else if (regPassField.Text == regPassRepField.Text)
            {
                User user = UserTable.GetUser(regLoginField.Text);
                if(user.login == null)
                {
                    user.login = regLoginField.Text;
                    user.password = regPassField.Text;
                    int a = UserTable.Insert(user);
                    if(a == 1)
                    {
                        MessageBox.Show("You have been registered succesfully!");
                        this.Close();
                        th = new Thread(OpenAuthorization);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();
                    }
                }
                else
                {
                    label5.Text = "Choose another login";
                    label5.Visible = true;
                }

            }
            else
            {
                label5.Text = "Passwords don't match!";
                label5.Visible = true;
            }
        }

        private void OpenAuthorization(object obj)
        {
            Application.Run(new Authorization());
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(OpenAuthorization);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
