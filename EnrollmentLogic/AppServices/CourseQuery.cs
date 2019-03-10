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
    public sealed class CourseQuery : IQuery<List<EnrollmentInfoDto>>
    {        
        public long Id { get; }

        public CourseQuery(long id)
        {
            Id = id;
        }

        internal sealed class GetListQueryHandler : IQueryHandler<CourseQuery, List<EnrollmentInfoDto>>
        {

            private readonly IDBQuerySessionFactory sessionQueryFactory;
            private readonly IDBSessionFactory sessionFactory;

            public GetListQueryHandler(IDBQuerySessionFactory sessionQueryFactory, IDBSessionFactory sessionFactory)
            {
                this.sessionQueryFactory = sessionQueryFactory;
                this.sessionFactory = sessionFactory;
            }

            public List<EnrollmentInfoDto> Handle(CourseQuery query)
            {
                var unitOfWork = new UnitOfWork(sessionQueryFactory);
                var enrollmentQueryRepository = new EnrollmentQueryRepository(unitOfWork);
                var uow = new UnitOfWork(this.sessionFactory);
                var courseRepository = new CourseRepository(uow);
                var studentRepository = new StudentRepository(uow);

                var result = (from c1 in courseRepository.GetById(query.Id)
                              join e1 in enrollmentQueryRepository.GetAll() on c1.Name equals e1.CourseName
                              select new { c1, e1 } into t1
                              group t1 by t1.e1.CourseName into g
                              select new EnrollmentInfoDto
                              {
                                  CourseName = g.Key,
                                  TeacherName = g.Select(s => s.c1.Name).FirstOrDefault(),
                                  MaxCapacity = g.Select(s => s.c1.Maximum).FirstOrDefault(),
                                  CurrentStucentCount = g.Count(),
                                  AverageAge = Convert.ToInt32(g.Average(s => s.e1.Age)),
                                  MinAge = g.Min(e => e.e1.Age),
                                  MaxAge = g.Max(e => e.e1.Age),
                                  Students = g.Select(s => new StudentDto
                                  {
                                      Id = s.e1.Student,
                                      Name = s.e1.Name,
                                      CourseName = s.c1.Name,
                                      Email = s.e1.Email,
                                      Age = s.e1.Age
                                  }).ToList()
                              }).ToList();
                return result;
            }
        }
    }
}
