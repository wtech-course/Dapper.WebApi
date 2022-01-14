using AutoMapper.Configuration.Annotations;
using Dapper.Contrib.Extensions;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dapper.Core.Entities
{
   public class ClassRoom
    {
       
        public int Id { get; set; }
        public string ClassRoom_Name { get; set; }
        public string School_Name { get; set; }
        public string Address { get; set; }
        [Description("Ignore")]

        [InnerJoin("Lesson", "Id", "ClassRoom_Id")]
        public  List<Lesson> Lessons { get; set; }
    }
}
