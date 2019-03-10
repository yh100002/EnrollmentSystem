using System;
using System.Collections.Generic;
using System.Linq;
using EnrollmentApi.Logic.Persistence;
using EnrollmentApi.Logic.Utils;

namespace EnrollmentApi.Logic.Students
{
    public sealed class StudentRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Student GetById(long id)
        {
            return _unitOfWork.Get<Student>(id);
        }

        public void Save(Student student)
        {
            _unitOfWork.SaveOrUpdate(student);
        }

        public void Delete(Student student)
        {
            _unitOfWork.Delete(student);
        }
    }


    public sealed class CourseRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public CourseRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Course GetByName(string name)
        {
            return _unitOfWork.Query<Course>().SingleOrDefault(x => x.Name == name);
        }

        public List<Course> GetById(long id)
        {
            return _unitOfWork.Query<Course>().Where(x => x.Id == id).ToList();
        }

        public bool IsFull(long courseId)
        {
            var capacity = _unitOfWork.Query<Course>().Where(w => w.Id == courseId).Select(s => s.Maximum).First();
            var test = _unitOfWork.Query<Enrollment>().Where(w => w.Course.Id == courseId)
                .GroupBy(g => g.Course, (key, group) => new { CurrentNumber = group.Count() }).FirstOrDefault();
            return test.CurrentNumber >= capacity;
        }


    }

    public sealed class TeacherRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public TeacherRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Teacher GetByName(string name)
        {
            return _unitOfWork.Query<Teacher>().SingleOrDefault(x => x.Name == name);
        }

        public Teacher GetById(long id)
        {
            return _unitOfWork.Query<Teacher>().SingleOrDefault(x => x.Id == id);
        }
    }

    public sealed class EnrollmentQueryRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public EnrollmentQueryRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public StudentQuery GetByName(string name)
        {
            return _unitOfWork.Query<StudentQuery>().SingleOrDefault(x => x.Name == name);
        }

        public List<StudentQuery> GetById(long id)
        {
            return _unitOfWork.Query<StudentQuery>().Where(x => x.Id == id).ToList();
        }

        public List<StudentQuery> GetAll()
        {
            return _unitOfWork.Query<StudentQuery>().ToList();
        }

        public void Save(StudentQuery student)
        {
            Console.WriteLine($"Save******************************:{student.Id},{student.Name}");
            _unitOfWork.SaveOrUpdate(student);            
        }
    }
}
