using System;
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
    public sealed class CoursesQuery : IQuery<List<EnrollmentInfoDto>>
    {              
        internal sealed class GetListQueryHandler : IQueryHandler<CoursesQuery, List<EnrollmentInfoDto>>
        {

            private readonly IDBQuerySessionFactory sessionQueryFactory;
            private readonly IDBSessionFactory sessionFactory;

            public GetListQueryHandler(IDBQuerySessionFactory sessionQueryFactory, IDBSessionFactory sessionFactory)
            {
                this.sessionQueryFactory = sessionQueryFactory;
                this.sessionFactory = sessionFactory;
            }

            public List<EnrollmentInfoDto> Handle(CoursesQuery query)
            {
                var unitOfWork = new UnitOfWork(sessionQueryFactory);
                var enrollmentQueryRepository = new EnrollmentQueryRepository(unitOfWork);
                var uow = new UnitOfWork(this.sessionFactory);
                var courseRepository = new CourseRepository(uow);
                var studentRepository = new StudentRepository(uow);

                var resultSet = enrollmentQueryRepository.GetAll()
                    .GroupBy(g => g.CourseName)
                    .Select(g => new EnrollmentInfoDto
                    {
                        CourseName = g.Key,
                        MaxAge = g.Max(r => r.Age),
                        MinAge = g.Min(r => r.Age),
                        AverageAge = Convert.ToInt32(g.Average(r => r.Age)),
                        TeacherName = g.Select(r => r.TeacherName).FirstOrDefault(),
                        CurrentStucentCount = g.Count(),
                        MaxCapacity = courseRepository.GetByName(g.Key).Maximum,
                        Students = g.Select(r => new StudentDto
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Email = r.Email,
                            Age = r.Age,
                            CourseName = r.CourseName

                        }).ToList()
                    });                

                return resultSet.ToList();
            }         
        }
    }
}
