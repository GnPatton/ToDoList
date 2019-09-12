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
    public partial class Account : Form
    {
        User user;
        Thread th;
        public Account(User user)
        {
            this.user = user;
            InitializeComponent();
            label1.Text ="Hi, " + user.login;
            FillToDoListDataGridView();
        }

        private void FillToDoListDataGridView()
        {
            toDoDataGridView.DataSource = ToDoListTable.GetList(user.userId).Tables[0];
            toDoDataGridView.Columns[0].Visible = false;
            toDoDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            toDoDataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (toDoDataGridView.Rows.Count != 0)
                toDoDataGridView.Rows[0].Cells[0].Selected = false;
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(OpenAuthorization);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void OpenAuthorization(object obj)
        {
            Application.Run(new Authorization());
        }

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            NewTask newTask = new NewTask(user);
            newTask.Owner = this;
            newTask.FormBorderStyle = FormBorderStyle.FixedSingle;
            newTask.MaximizeBox = false;
            newTask.ShowDialog();

            if (DialogResult.OK == newTask.DialogResult)
            {
                FillToDoListDataGridView();
            }
        }

        private void toDoDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = toDoDataGridView.Rows[index];
            int id = Convert.ToInt32(selectedRow.Cells[0].Value);
            EditTask editTask = new EditTask(id);
            editTask.Owner = this;
            editTask.FormBorderStyle = FormBorderStyle.FixedSingle;
            editTask.MaximizeBox = false;
            editTask.ShowDialog();

            if (DialogResult.OK == editTask.DialogResult)
            {
                FillToDoListDataGridView();
            }

            if (DialogResult.Abort == editTask.DialogResult)
            {
                FillToDoListDataGridView();
            }
        }
    }
}
