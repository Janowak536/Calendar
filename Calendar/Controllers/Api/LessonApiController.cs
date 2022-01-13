using Calendar.Models.ViewModels;
using Calendar.Services;
using Calendar.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Calendar.Controllers.Api
{
    [Route("api/Lesson")]
    [ApiController]
    public class LessonApiController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public LessonApiController(ILessonService lessonService, IHttpContextAccessor httpContextAccessor)
        {
            _lessonService = lessonService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(LessonVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _lessonService.AddUpdate(data).Result;
                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.lessonUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.lessonAdded;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string teacherId)
        {
            CommonResponse<List<LessonVM>> commonResponse = new CommonResponse<List<LessonVM>>();
            try
            {
                if (role == Helper.Student)
                {
                    commonResponse.dataenum = _lessonService.StudentsEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else if (role == Helper.Teacher)
                {
                    commonResponse.dataenum = _lessonService.TeachersEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else
                {
                    commonResponse.dataenum = _lessonService.TeachersEventsById(teacherId);
                    commonResponse.status = Helper.success_code;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            CommonResponse<LessonVM> commonResponse = new CommonResponse<LessonVM>();
            try
            {

                commonResponse.dataenum = _lessonService.GetById(id);
                commonResponse.status = Helper.success_code;

            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }


    }
}
