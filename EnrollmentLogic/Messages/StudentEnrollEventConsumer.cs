using EnrollmentApi.Logic.Events;
using EnrollmentApi.Logic.Interfaces;
using EnrollmentApi.Logic.Persistence;
using EnrollmentApi.Logic.Students;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrollmentApi.Logic.Messages
{
    public class StudentEnrollEventConsumer : IConsumer<StudentEnrollEvent>
    {        
        private readonly IDBQuerySessionFactory _sessionQueryFactory;


        public StudentEnrollEventConsumer(IDBQuerySessionFactory sessionQueryFactory)
        {            
            _sessionQueryFactory = sessionQueryFactory;
        }

        public async Task Consume(ConsumeContext<StudentEnrollEvent> context)
        {            
            var unitOfWork = new UnitOfWork(_sessionQueryFactory);
            var enrollmentQueryRepository = new EnrollmentQueryRepository(unitOfWork);
            var students = enrollmentQueryRepository.GetById(context.Message.Id);
            //Console.WriteLine($"students.Count===>{students.First().Name}");
            //Console.WriteLine($"context.Message.Id===>{context.Message.Id}");
            var student = new StudentQuery(context.Message.Id, context.Message.Name, 
                context.Message.Email, context.Message.Age, students.Count + 1, 
                context.Message.Course, context.Message.TeacherName);

            enrollmentQueryRepository.Save(student);

            unitOfWork.Commit();

            Console.WriteLine($"StudentEnrollEventConsumer:{context.Message.Course}");
        }
    }
}
