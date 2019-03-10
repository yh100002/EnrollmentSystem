using System.Collections.Generic;
using System.Linq;
using Dapper;
using EnrollmentApi.Logic.Dtos;
using EnrollmentApi.Logic.Interfaces;
using EnrollmentApi.Logic.Persistence;
using EnrollmentApi.Logic.Students;
using EnrollmentApi.Logic.Utils;

namespace EnrollmentApi.Logic.AppServices
{
    public sealed class StudentQuery : IQuery<List<StudentDto>>
    {
        public string EnrolledIn { get; }        

        public StudentQuery(string enrolledIn)
        {
            EnrolledIn = enrolledIn;            
        }

        internal sealed class GetListQueryHandler : IQueryHandler<StudentQuery, List<StudentDto>>
        {           

            private readonly IDBQuerySessionFactory _sessionFactory;

            public GetListQueryHandler(IDBQuerySessionFactory sessionFactory)
            {
                this._sessionFactory = sessionFactory;
            }

            public List<StudentDto> Handle(StudentQuery query)
            {
                var unitOfWork = new UnitOfWork(_sessionFactory);
                var enrollmentQueryRepository = new EnrollmentQueryRepository(unitOfWork);

                if(string.IsNullOrEmpty(query.EnrolledIn))
                {
                   var students = enrollmentQueryRepository.GetAll()
                   .Select(s => new StudentDto()
                   {
                       Id = s.Student,
                       Name = s.Name,
                       Email = s.Email,
                       Age = s.Age,
                       CourseName = s.CourseName
                   }
                   ).ToList();
                   return students;
                }
                else
                {
                   var students = enrollmentQueryRepository.GetAll()
                    .Where(w => w.CourseName == query.EnrolledIn)
                    .Select(s => new StudentDto()
                    {
                        Id = s.Student,
                        Name = s.Name,
                        Email = s.Email,
                        Age = s.Age,
                        CourseName = s.CourseName
                    }
                    ).ToList();
                    return students;
                }
            }
        }
    }
}
