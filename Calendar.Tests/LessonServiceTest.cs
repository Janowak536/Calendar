using Calendar.Api.Views.ViewModels;
using Calendar.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calendar.Tests
{
    public class LessonServiceTest
    {
        private readonly Mock<ILessonService> lessonService;

        public LessonServiceTest()
        {
            lessonService = new Mock<ILessonService>();
        }

        [Fact]
        public void VerifyIfStudentListReturnsNotEmptyCollection()
        {
            //Arange

            //Act

            var studentList = lessonService.Setup(x => x.GetStudentList())
                                .Returns();

            //Assert

            Assert.NotEmpty(studentList);
        }

        private List<StudentVM> GetSampleEmployee()
        {
            List<StudentVM> output = new List<StudentVM>
        {
            new Employee
            {
                FirstName = "Jhon",
                LastName = "Doe",
                PhoneNumber = "01682616789",
                DateOfBirth = DateTime.Now,
                Email = "jhon@gmail.com",
                EmployeeId = 1
            }
        };
            return output;
        }
    }
}
