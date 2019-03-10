using System;
using System.Collections.Generic;
using System.Linq;


namespace EnrollmentApi.Logic.Students
{
    public class StudentQuery : Entity
    {
        public virtual long Student { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual int Age { get; set; }
        public virtual long NumberOfEnrollments { get; set; }
        public virtual string CourseName { get; set; }
        public virtual string TeacherName { get; set; }

        protected StudentQuery()
        {
        }

        public StudentQuery(long id, string name, string email, int age, long numberOfEnrollments, string courseName, string teacherName) : this()
        {
            Student = id;
            Name = name;
            Email = email;
            Age = age;
            NumberOfEnrollments = numberOfEnrollments;
            CourseName = courseName;
            TeacherName = teacherName;
        }
    }
}
