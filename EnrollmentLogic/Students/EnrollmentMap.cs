using FluentNHibernate.Mapping;

namespace EnrollmentApi.Logic.Students
{
    public class EnrollmentMap : ClassMap<Enrollment>
    {
        public EnrollmentMap()
        {
            Id(x => x.Id);           

            References(x => x.Student);
            References(x => x.Course);            
        }
    }
}
