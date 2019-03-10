using System;
using System.Collections.Generic;
using System.Text;

namespace EnrollmentApi.Logic.Events
{
    public class StudentEnrollEvent : IntegrationEvent
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

        public string TeacherName { get; set; }       

        public StudentEnrollEvent(long id, string name, string email, int age, string course, string teacherName)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Age = age;
            this.TeacherName = teacherName;
            this.Course = course;           
        }
    }
}

