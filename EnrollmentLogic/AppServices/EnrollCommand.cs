using System;
using CSharpFunctionalExtensions;
using EnrollmentApi.Logic.Events;
using EnrollmentApi.Logic.Interfaces;
using EnrollmentApi.Logic.Persistence;
using EnrollmentApi.Logic.Students;
using EnrollmentApi.Logic.Utils;
using MassTransit;

namespace EnrollmentApi.Logic.AppServices
{
    public sealed class EnrollCommand : ICommand
    {
        public long Id { get; }
        public string CourseName { get; }        

        public EnrollCommand(long id, string courseName)
        {
            Id = id;
            CourseName = courseName;            
        }

        internal sealed class EnrollCommandHandler : ICommandHandler<EnrollCommand>
        {
            private readonly IDBSessionFactory _sessionFactory;
            private readonly IBus _messageBus;

            public EnrollCommandHandler(IDBSessionFactory sessionFactory, IBus messageBus)
            {
                _sessionFactory = sessionFactory;
                this._messageBus = messageBus;
            }

            public Result Handle(EnrollCommand command)
            {
                var unitOfWork = new UnitOfWork(_sessionFactory);
                var courseRepository = new CourseRepository(unitOfWork);
                var studentRepository = new StudentRepository(unitOfWork);
                var teacherRepository = new TeacherRepository(unitOfWork);
                Student student = studentRepository.GetById(command.Id);
                if (student == null)
                    return Result.Fail($"No student found with Id '{command.Id}'");

                Course courseObj = courseRepository.GetByName(command.CourseName);
                if (courseObj == null)
                    return Result.Fail($"Course is incorrect: '{command.CourseName}'");

                Teacher teacher = teacherRepository.GetByName(courseObj.Teacher.Name);
                if (courseObj == null)
                    return Result.Fail($"Teacher is incorrect: {courseObj.Teacher.Name} 'in {command.CourseName}'");
                                
                bool isFull = courseRepository.IsFull(courseObj.Id);
                if (isFull)
                {
                    return Result.Fail($"The Course is Full already in {command.CourseName}!!!!");
                }
                else
                {
                    string course = courseObj.Name;
                    string teacherName = courseObj.Teacher.Name;
                    this._messageBus.Publish<StudentEnrollEvent>(new
                    {
                        student.Id,
                        student.Name,                        
                        student.Email,
                        student.Age,
                        course,
                        teacherName
                    }
                   );
                    student.Enroll(courseObj);
                    unitOfWork.Commit();
                    return Result.Ok("The Student was enrolled!!");
                }
               
            }
        }
    }
}