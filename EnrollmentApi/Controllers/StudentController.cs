using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnrollmentApi.Logic.AppServices;
using EnrollmentApi.Logic.Dtos;
using EnrollmentApi.Logic.Interfaces;
using EnrollmentApi.Logic.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentApi.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly IMessages messages;

        public StudentController(IMessages messages)
        {
            this.messages = messages;
        }

        [HttpGet("getStudentsAllInEnrolled")]
        public IActionResult GetStudentsAllInEnrolled()
        {
            List<StudentDto> list = this.messages.Dispatch(new StudentQuery(null));
            return Ok(list);
        }

        [HttpGet("getStudentsInSpecific")]
        public IActionResult GetStudentsInSpecific(string enrolledIn)
        {
            List<StudentDto> list = this.messages.Dispatch(new StudentQuery(enrolledIn));
            return Ok(list);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
            Console.WriteLine(dto.Name);
            var command = new RegisterCommand(dto.Name, dto.Email, dto.Age, dto.CourseName);

            Result result = this.messages.Dispatch(command);
            return FromResult(result);
        }     

        
      
    }
}
