using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Dapper.Core.Entities
{
   public  class Lesson
    {
       
        public int Id { get; set; }
        public int ClassRoom_Id { get; set; }
        public string Lesson_Name { get; set; }
        public int Lesson_Time { get; set; }
        public string Lesson_Teacher_Name { get; set; }
        [Description("Ignore")]
        public ICollection<Student> Students { get; set; }

        
    }
}
