using FluentNHibernate.Mapping;

namespace EnrollmentApi.Logic.Students
{
    public class CourseMap : ClassMap<Course>
    {
        public CourseMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Maximum);
            References(x => x.Teacher);
        }
    }
}
