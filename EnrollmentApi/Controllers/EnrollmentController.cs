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
    [Route("api/enrollment")]
    public sealed class EnrollmentController : BaseController
    {
        private readonly IMessages messages;

        public EnrollmentController(IMessages messages)
        {
            this.messages = messages;
        }      

        [HttpGet("getCourses")]
        public IActionResult GetCourses()
        {
            List<EnrollmentInfoDto> list = this.messages.Dispatch(new CoursesQuery());
            return Ok(list);
        }

        [HttpGet("getCourse/{id}")]
        public IActionResult GetCourse(long id)
        {
            List<EnrollmentInfoDto> list = this.messages.Dispatch(new CourseQuery(id));
            return Ok(list);
        }


        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollmentDto dto)
        {
            Result result = this.messages.Dispatch(new EnrollCommand(id, dto.CourseName));
            return FromResult(result);
        }

    }
}
