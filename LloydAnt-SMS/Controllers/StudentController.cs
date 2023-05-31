using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Mvc;

namespace LloydAnt_SMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IUserHelper _userHelper;

        public StudentController(AppDbContext db, IUserHelper userHelper)
        {
            _db = db;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            var username = User.Identity.Name;
            var model = _userHelper.FindByUserNameAsync(username).Result;
            return View(model);
        }


        [HttpGet]        
        public IActionResult CourseRegistration()
        {
            ViewBag.AllCourses = _userHelper.DropdownOfCourses();
            var userData = _userHelper.FindByUserNameAsync(User.Identity.Name).Result;
            return View(userData);
        }

        [HttpPost]
        public async Task<JsonResult> RegistrationCourse(CourseResigtrationViewModel course)
        {
            var userName = User.Identity.Name;
            if(userName != null)
            {
                var user = _userHelper.FindByUserNameAsync(userName).Result;
                if (course.CourseIds.Any() && user != null)
                {
                    course.UserId = user.Id;
                    var result = await _userHelper.NewStudentCourseRegistration(course);
                    if (result)
                    {
                        return Json(new { isError = false, msg = "Successfull" });
                    }
                }
            }
            return Json(new { isError = true, msg = "Failed" });

        }
    }
}
