using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Student = "Student";
        public static string Teacher = "Teacher";
        public static string lessonAdded = "Lesson added successfully.";
        public static string lessonUpdated = "Lesson updated successfully.";
        public static string lessonDeleted = "Lesson deleted successfully.";
        public static string lessonExists = "Lesson for selected date and time already exists.";
        public static string lessonNotExists = "Lesson not exists.";

        public static string meetingConfirm = "Meeting confirm successfully.";
        public static string meetingConfirmError = "Error while confirming meeting";


        public static string lessonAddError = "Something went wront, Please try again.";
        public static string lessonUpdatError = "Something went wront, Please try again.";
        public static string somethingWentWrong = "Something went wront, Please try again.";
        public static int success_code = 1;
        public static int failure_code = 0;

        public static List<SelectListItem> GetRolesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.Admin,Text=Helper.Admin},
                new SelectListItem{Value=Helper.Student,Text=Helper.Student},
                new SelectListItem{Value=Helper.Teacher,Text=Helper.Teacher}
            };
        }

        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr" });
                minute = minute + 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr 30 min" });
                minute = minute + 30;
            }
            return duration;
        }
    }
}
