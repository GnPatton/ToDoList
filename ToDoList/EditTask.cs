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
    public partial class EditTask : Form
    {
        private int taskId;

        public EditTask(int id)
        {
            InitializeComponent();
            this.taskId = id;
            textBox1.Text = ToDoListTable.GetTask(id).Task;
            comboBox1.SelectedItem = ToDoListTable.GetTask(id).Priority;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Database.ToDoList list = new Database.ToDoList
            {
                Id = taskId,
                Task = textBox1.Text,
                Priority = comboBox1.SelectedItem.ToString()
            };
            ToDoListTable.UpdateTask(list);
            this.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int a = ToDoListTable.DeleteTask(taskId);
            this.Close();
        }

    }
}
