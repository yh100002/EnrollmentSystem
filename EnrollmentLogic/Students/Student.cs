using System;
using System.Collections.Generic;
using System.Linq;

namespace EnrollmentApi.Logic.Students
{
    public class Student : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }

        public virtual int Age { get; set; }

        private readonly IList<Enrollment> _enrollments = new List<Enrollment>();
        public virtual IReadOnlyList<Enrollment> Enrollments => _enrollments.ToList();     

        protected Student()
        {
        }

        public Student(string name, string email, int age)
            : this()
        {
            Name = name;
            Email = email;
            Age = age;
        }

        public virtual Enrollment GetEnrollment(int index)
        {
            if (_enrollments.Count > index)
                return _enrollments[index];

            return null;
        }

        public virtual void RemoveEnrollment(Enrollment enrollment, string comment)
        {
            _enrollments.Remove(enrollment);         
        }

        public virtual void Enroll(Course course)
        {
            var enrollment = new Enrollment(this, course);
            _enrollments.Add(enrollment);
        }
    }
}
