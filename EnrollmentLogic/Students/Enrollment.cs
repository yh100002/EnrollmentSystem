namespace EnrollmentApi.Logic.Students
{
    public class Enrollment : Entity
    {
        public virtual Student Student { get; protected set; }
        public virtual Course Course { get; protected set; }        

        protected Enrollment()
        {
        }

        public Enrollment(Student student, Course course)
            : this()
        {
            Student = student;
            Course = course;            
        }

        public virtual void Update(Course course)
        {
            Course = course;            
        }

    }
}
