using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Student_Name { get; set; }
        public int Student_Number { get; set; }
        public string Student_SurName { get; set; }
        public string Phone_Number { get; set; }
    }
}
