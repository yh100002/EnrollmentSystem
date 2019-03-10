using System;
using CSharpFunctionalExtensions;
using EnrollmentApi.Logic.Decorators;
using EnrollmentApi.Logic.Students;
using EnrollmentApi.Logic.Persistence;
using EnrollmentApi.Logic.Interfaces;
using EnrollmentApi.Logic.Events;
using MassTransit;

namespace EnrollmentApi.Logic.AppServices
{
    public sealed class RegisterCommand : ICommand
    {
        public string Name { get; }
        public string Email { get; }
        public int Age { get; }
        public string Course { get; }        

        public RegisterCommand(string name, string email, int age, string course)
        {
            Name = name;
            Email = email;
            Age = age;
            Course = course;
            
        }

        [AuditLog]
        internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
        {
            private readonly IDBSessionFactory _sessionFactory;
            private readonly IBus _messageBus;

            public RegisterCommandHandler(IDBSessionFactory sessionFactory, IBus messageBus)
            {
                _sessionFactory = sessionFactory;
                _messageBus = messageBus;
            }

            public Result Handle(RegisterCommand command)
            {
                var unitOfWork = new UnitOfWork(_sessionFactory);
                var courseRepository = new CourseRepository(unitOfWork);
                var studentRepository = new StudentRepository(unitOfWork);
                var student = new Student(command.Name, command.Email, command.Age);

                if (command.Course != null)
                {
                    Course course = courseRepository.GetByName(command.Course);
                    if(course == null)
                    {
                        return Result.Fail("The course does not exist!");
                    }

                    bool isFull = courseRepository.IsFull(course.Id);                             

                    if (!isFull)
                    {                       
                        studentRepository.Save(student);
                        unitOfWork.Commit();
                        this._messageBus.Publish<StudentRegisterEvent>(new
                        {
                            command.Name,
                            command.Email,
                            command.Age,
                            command.Course
                        }
                       );

                        return Result.Ok();
                    }
                    else
                    {                       
                       return Result.Fail("The course is already full!! So registration not allowed.");
                    }                    
                }
                return Result.Fail("The course does not exist!");
            }
        }
    }
}
