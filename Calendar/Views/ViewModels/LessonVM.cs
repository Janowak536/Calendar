using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Api.Views.ViewModels
{
    public class LessonVM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Duration { get; set; }
        public string TeacherId { get; set; }
        public string StudentId { get; set; }
        public bool IsTeacherApproved { get; set; }
        public string AdminId { get; set; }

        public string TeacherName { get; set; }
        public string StudentName { get; set; }
        public string AdminName { get; set; }
        public bool IsForClient { get; set; }
    }
}
