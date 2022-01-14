using Dapper.Application.Interfaces;
using Dapper.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IClassRoomRepository, ClassRoomRepository>();
            services.AddTransient<ILessonRepository, LessonRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
