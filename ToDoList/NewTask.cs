using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoList.Database.dao_sqls;
using ToDoList.Database;

namespace ToDoList
{
    public partial class NewTask : Form
    {
        User user;

        public NewTask(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.Year + "-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("dd");
            ToDoListTable.Insert(new Database.ToDoList { Date = date, Task = taskTextBox.Text, Priority = comboBox1.SelectedItem.ToString() }, user);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
