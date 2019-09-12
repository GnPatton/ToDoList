using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Database
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }
        public string Priority { get; set; }
    }
}
