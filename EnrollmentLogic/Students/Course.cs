namespace EnrollmentApi.Logic.Students
{
    public class Course : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual int Maximum { get; protected set; }
        public virtual Teacher Teacher { get; protected set; }

        protected Course()
        {
        }

        public Course(Teacher teacher): this()
        {
            Teacher = teacher;
        }
    }
}
