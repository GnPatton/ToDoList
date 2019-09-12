using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Database.dao_sqls
{
    public class ToDoListTable
    {
        private static String SQL_SELECT_ALL = "SELECT id, date, task, priority FROM todolist WHERE user_id=@user_id";
        private static String SQL_SELECT_TASK = "SELECT task, priority FROM todolist WHERE id=@taskId";
        private static String SQL_UPDATE = "UPDATE todolist SET task=@task, priority=@priority WHERE id=@taskId";
        private static String SQL_INSERT = "INSERT INTO todolist(date, task, priority, user_id) VALUES(@date, @task, @priority, @userId)";
        private static String SQL_DELETE = "DELETE FROM todolist WHERE id=@taskId";

        public static DataSet GetList(int userId)
        {
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_SELECT_ALL);
            command.Parameters.AddWithValue("@user_id", userId);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            db.Close();
            return ds;
        }

        public static ToDoList GetTask(int taskId)
        {
            ToDoList task = new ToDoList();
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_SELECT_TASK);
            command.Parameters.AddWithValue("@taskId", taskId);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                task.Task = Convert.ToString(reader["task"]);
                task.Priority = Convert.ToString(reader["priority"]);
            }
            db.Close();
            return task;
        }

        public static void UpdateTask(ToDoList list)
        {
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@taskId", list.Id);
            command.Parameters.AddWithValue("@task", list.Task);
            command.Parameters.AddWithValue("@priority", list.Priority);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
        }

        public static int DeleteTask(int taskId)
        {
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue("@taskId", taskId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }


        public static int Insert(ToDoList list, User user)
        {
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@date", list.Date);
            command.Parameters.AddWithValue("@task", list.Task);
            command.Parameters.AddWithValue("@priority", list.Priority);
            command.Parameters.AddWithValue("@userId", user.userId);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
    }
}
