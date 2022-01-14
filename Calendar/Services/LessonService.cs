using Calendar.Models;
using Calendar.Models.ViewModels;
using Calendar.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Services
{
    public class LessonService : ILessonService
    {
        private readonly ApplicationDbContext _db;

        public LessonService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(LessonVM model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));

            if (model != null && model.Id > 0)
            {
                var lesson = _db.Lessons.FirstOrDefault(x => x.Id == model.Id);
                lesson.Title = model.Title;
                lesson.Description = model.Description;
                lesson.StartDate = startDate;
                lesson.EndDate = endDate;
                lesson.Duration = model.Duration;
                lesson.TeacherId = model.TeacherId;
                    lesson.StudentId = model.StudentId;
                    lesson.IsTeacherApproved = false;
                    lesson.AdminId = model.AdminId;

                await _db.SaveChangesAsync();
                return 1;
            }
            else
            {
                Lesson lesson = new Lesson()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    TeacherId = model.TeacherId,
                    StudentId = model.StudentId,
                    IsTeacherApproved = false,
                    AdminId = model.AdminId
                };

                _db.Lessons.Add(lesson);
                await _db.SaveChangesAsync();
                return 2;
            }

        }

        public List<LessonVM> TeachersEventsById(string teacherId)
        {
            return _db.Lessons.Where(x => x.TeacherId == teacherId).ToList().Select(c => new LessonVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsTeacherApproved = c.IsTeacherApproved
            }).ToList();
        }

        public LessonVM GetById(int id)
        {
            return _db.Lessons.Where(x => x.Id == id).ToList().Select(c => new LessonVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsTeacherApproved = c.IsTeacherApproved,
                StudentId = c.StudentId,
                TeacherId = c.TeacherId,
                StudentName = _db.Users.Where(x => x.Id == c.StudentId).Select(x => x.Name).FirstOrDefault(),
                TeacherName = _db.Users.Where(x => x.Id == c.TeacherId).Select(x => x.Name).FirstOrDefault(),
            }).SingleOrDefault();
        }

        public List<TeacherVM> GetTeacherList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Teacher) on userRoles.RoleId equals roles.Id
                           select new TeacherVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return doctors;
        }

        public List<StudentVM> GetStudentList()
        {
            var students = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == Helper.Student) on userRoles.RoleId equals roles.Id
                            select new StudentVM
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                           ).ToList();

            return students;
        }

        public List<LessonVM> StudentsEventsById(string studentId)
        {
            return _db.Lessons.Where(x => x.StudentId == studentId).ToList().Select(c => new LessonVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsTeacherApproved = c.IsTeacherApproved
            }).ToList();
        }

        public async Task<int> Delete(int id)
        {

            var lesson = _db.Lessons.FirstOrDefault(x => x.Id == id);
            if (lesson != null)
            {
                _db.Lessons.Remove(lesson);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> ConfirmEvent(int id)
        {
            var lesson = _db.Lessons.FirstOrDefault(x => x.Id == id);
            if(lesson != null)
            {
                lesson.IsTeacherApproved = true;
                return await _db.SaveChangesAsync();
            }
            return 0;
        }
    }
}