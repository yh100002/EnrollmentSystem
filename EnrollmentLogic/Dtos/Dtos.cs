using System.Collections.Generic;
using System;
using System.Collections.Generic;
using Nest;

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

     /*
    Total number of requests processed
    Total number of requests resulted in an OK, 4xx and 5xx responses
    Average response time of all requests
    Min response time of all requests
    Max response time of all requests */

    public class LogAggregatedValueDto
    {
        public double? TotalCountOfAllRequest { get; set; }
        public double? TotalCountOfNormal { get; set; }
        public double? TotalCountError { get; set; }           
        public double? AverageDurationForAllRequest { get; set; }
        public double? MinDurationForAllRequest { get; set; }
        public double? MaxDurationForAllRequest { get; set; }
        public Dictionary<string, int> CountOfEachErrors { get; set; } = new Dictionary<string, int>();
    }   
    
    public class LogResponseDto
    {
        public string Message { get; set; }

        [Text(Name = "level")]
        public string Level { get; set; }

        [Date(Name = "@timestamp")]
        public DateTime Timestamp { get; set; }
        public LogResponseFields Fields { get; set; }        
    }

    public class LogResponseFields
    {            
        public string Operation { get; set; }
        
        [Number(Name = "Duration")]
        public double Duration { get; set; }
        [Text(Name = "SourceContext.keyword")]
        public string SourceContext { get; set; }
    }


}
