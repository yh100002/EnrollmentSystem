using System;
using System.Collections.Generic;
using System.Text;

namespace EnrollmentApi.Logic.Events
{
    public class StudentRegisterEvent : IntegrationEvent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }

        public StudentRegisterEvent(long id, string name, string email, int age, string courseName, string teacherName)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Age = age;
            this.CourseName = courseName;
            this.TeacherName = teacherName;
        }
    }
}
