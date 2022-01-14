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
        public static string lessonAdded = "Lekcja dodana pomyślnie.";
        public static string lessonUpdated = "Szczegóły lekcji zostały zmienione.";
        public static string lessonDeleted = "Lekcja usunięta pomyślnie.";
        public static string lessonExists = "Taka lekcja już istnieje.";
        public static string lessonNotExists = "Taka lekcja nie istnieje.";

        public static string meetingConfirm = "Lekcja została potwierdzona.";
        public static string meetingConfirmError = "Błąd podczas potwierdzania lekcji";


        public static string lessonAddError = "Coś poszło nie tak, spróbuj ponownie.";
        public static string lessonUpdatError = "Coś poszło nie tak, spróbuj ponownie.";
        public static string somethingWentWrong = "Coś poszło nie tak, spróbuj ponownie.";
        public static int success_code = 1;
        public static int failure_code = 0;

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.Admin,Text=Helper.Admin}
            };
            }
            else
            {
                return new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.Student,Text=Helper.Student},
                new SelectListItem{Value=Helper.Teacher,Text=Helper.Teacher}
            };
            }
            
        }

        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Godz." });
                minute = minute + 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Godz. 30 min" });
                minute = minute + 30;
            }
            return duration;
        }
    }
}
