using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Mvc;

namespace LloydAnt_SMS.Controllers
{
    public class AdminController : Controller
    {

		private readonly AppDbContext _db;
		private readonly IUserHelper _userHelper;

		public AdminController(AppDbContext db, IUserHelper userHelper)
		{
			_db = db;
			_userHelper = userHelper;
		}

		[HttpGet]
		public IActionResult Index()
        {
            var model = new AdminDashBoardViewModel();
            model.Students = _userHelper.GetAllStudentsFromDB();
            return View(model);
        }
		[HttpGet]
		public IActionResult Courses()
		{
			var model = new List<SchCourses>();
			model = _userHelper.GetAllCourseFromDB().Where(c => !c.Deactivated).ToList();
			return View(model);
		}

		[HttpPost]
		public async Task<JsonResult> CoursePostAction(SchCourses course)
		{
			var username = User.Identity.Name;
			if (course != null && username != null)
			{ 
				var user = _userHelper.FindByUserNameAsync(username).Result;
				var checkIfExist = _db.SchCourses.Where(a => a.Name.ToLower() == course.Name.ToLower()).FirstOrDefault();
				if (checkIfExist != null)
				{
					return Json(new { isError = true, msg = "Course Already Exist" });
				}
				if (user == null)
				{
					return Json(new { isError = true, msg = "Failed" });
				}
				course.CreatedBy = user.Id;
				var bank = await _userHelper.NewCourseRegistration(course).ConfigureAwait(false);
				if (bank)
				{
					return Json(new { isError = false, msg = "Created Successfully" });
				}
			}
			return Json(new { isError = true, msg = "Failed" });

		}
	}
}
