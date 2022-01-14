using Dapper.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IClassRoomRepository classRoomRepository,ILessonRepository lessonRepository,IStudentRepository studentRepository)
        {
        
            ClassRoom = classRoomRepository;
            Lesson = lessonRepository;
            Student = studentRepository;

        }
        public ILessonRepository Lesson { get; }
        public IClassRoomRepository ClassRoom { get;}
        public IStudentRepository Student { get; }
    }
}
