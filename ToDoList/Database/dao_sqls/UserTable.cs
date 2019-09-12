using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Database.dao_sqls
{
    public class UserTable
    {
        private static String SQL_INSERT = "INSERT INTO user(login, password) VALUES(@login, @password)";
        private static String SQL_SELECT = "SELECT id, login, password FROM user WHERE login=@login AND password=@password";
        private static String SQL_SELECT_ALL = "SELECT login, password FROM user WHERE login=@login";

        public static int Insert(User user)
        {
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.AddWithValue("@login", user.login);
            command.Parameters.AddWithValue("@password", user.password);
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public static User GetUser(string userLogin, string userPassword)
        {
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.AddWithValue("@login", userLogin);
            command.Parameters.AddWithValue("@password", userPassword);
            SQLiteDataReader reader = command.ExecuteReader();
            User user = new User();
            while (reader.Read())
            {
                user.userId = Convert.ToInt32(reader["id"]);
                user.login = Convert.ToString(reader["login"]);
                user.password = Convert.ToString(reader["password"]);
            }
            return user;
        }

        public static User GetUser(string userLogin)
        {
            Database db = new Database();
            SQLiteCommand command = db.CreateCommand(SQL_SELECT_ALL);
            command.Parameters.AddWithValue("@login", userLogin);
            SQLiteDataReader reader = command.ExecuteReader();
            User user = new User();
            while (reader.Read())
            {
                user.login = Convert.ToString(reader["login"]);
            }
            return user;
        }
    }
}
