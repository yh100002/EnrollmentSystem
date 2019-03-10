using System.Collections.Generic;

namespace EnrollmentApi.Logic.Dtos
{
    public sealed class StudentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string CourseName { get; set; }
       
    }

    public sealed class NewStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string CourseName { get; set; }       
    }

    public sealed class StudentEnrollmentDto
    {
        public string CourseName { get; set; }        
    }

    public sealed class EnrollmentInfoDto
    {       
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentStucentCount { get; set; }
        public int AverageAge { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();

    }


}
