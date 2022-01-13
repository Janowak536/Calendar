using Calendar.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Services
{
    public interface ILessonService
    {
        public List<TeacherVM> GetTeacherList();
        public List<StudentVM> GetStudentList();
        public Task<int> AddUpdate(LessonVM model);

        public List<LessonVM> TeachersEventsById(string teacherId);

        public List<LessonVM> StudentsEventsById(string studentId);

        public LessonVM GetById(int id);

        public Task<int> Delete(int id);

        public Task<int> ConfirmEvent(int id);
    }
}
