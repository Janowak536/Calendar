﻿using Calendar.Models;
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
                //update
                return 1;
            }
            else
            {
                //create
                Lesson lesson = new Lesson()
                {
                    Topic = model.Topic,
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

        public Task<int> ConfirmEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public LessonVM GetById(int id)
        {
            throw new NotImplementedException();
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

        public List<TeacherVM> GetTeacherList()
        {
            var teachers = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Teacher) on userRoles.RoleId equals roles.Id
                           select new TeacherVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                            ).ToList();

            return teachers;
        }

        public List<LessonVM> StudentsEventsById(string studentId)
        {
            return _db.Lessons.Where(x => x.StudentId == studentId).ToList().Select(c => new LessonVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Topic = c.Topic,
                Duration = c.Duration,
                IsTeacherApproved = c.IsTeacherApproved
            }).ToList();
        }

        public List<LessonVM> TeachersEventsById(string teacherId)
        {
            return _db.Lessons.Where(x => x.TeacherId == teacherId).ToList().Select(c => new LessonVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Topic = c.Topic,
                Duration = c.Duration,
                IsTeacherApproved = c.IsTeacherApproved
            }).ToList();
        }
    }
}
