using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Models
{
    public class ClassRoomDTO
    {
        public int Id { get; set; }
        public string ClassRoom_Name { get; set; }
        public string School_Name { get; set; }
        public string Address { get; set; }
    }
}
