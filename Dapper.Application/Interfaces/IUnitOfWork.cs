using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IClassRoomRepository ClassRoom { get; }
        ILessonRepository Lesson { get; }
        IStudentRepository Student { get; }
    }
}
