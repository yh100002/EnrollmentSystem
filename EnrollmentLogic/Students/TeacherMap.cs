using FluentNHibernate.Mapping;

namespace EnrollmentApi.Logic.Students
{
    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);            
        }
    }
}
