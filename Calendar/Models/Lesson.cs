using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string TeacherId { get; set; }
        public string StudentId { get; set; }
        public bool IsTeacherApproved { get; set; }
        public string AdminId { get; set; }
    }
}
