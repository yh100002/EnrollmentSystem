using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace EnrollmentApi.Logic.Students
{
    public class StudentQueryMap : ClassMap<StudentQuery>
    {
        public StudentQueryMap()
        {
            Id(x => x.Id);
            Map(x => x.Student);
            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Age);
            Map(x => x.NumberOfEnrollments);
            Map(x => x.CourseName);
            Map(x => x.TeacherName);
        }
    }
}
