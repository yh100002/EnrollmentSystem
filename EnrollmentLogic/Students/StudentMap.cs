using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace EnrollmentApi.Logic.Students
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Email);
            Map(x => x.Age);
            HasMany(x => x.Enrollments).Access.CamelCaseField(Prefix.Underscore).Inverse().Cascade.AllDeleteOrphan();                        
        }
    }
}

