using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList.Database
{
    class Database
    {
        private SQLiteConnection Connection { get; set; }

        public Database()
        {
            string test = System.IO.Directory.GetCurrentDirectory().Replace("ToDoList\\bin\\Debug", "ProjectDB.db");
            Connection = new SQLiteConnection();
            Connection.ConnectionString = "data source=" + test;
            Connection.Open();
        }

        public SQLiteCommand CreateCommand(string strCommand)
        {
            SQLiteCommand cmd = new SQLiteCommand(strCommand, Connection);
            return cmd;
        }

        public int ExecuteNonQuery(SQLiteCommand command)
        {
            int rowNumber = 0;
            try
            {
                rowNumber = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            return rowNumber;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
