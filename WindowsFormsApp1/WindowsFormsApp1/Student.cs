using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Student
    {
        public int id { get; set; }

     
        public string name { get; set; }

        public int? age { get; set; }

        public int? deptid { get; set; }

        public  department department { get; set; }


    }
}
