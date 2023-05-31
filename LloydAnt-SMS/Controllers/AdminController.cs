using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LloydAnt_SMS.Controllers
{
	[Authorize(Roles = "Admin")]
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
					if (checkIfExist.Deactivated)
					{
						checkIfExist.Deactivated = false;
						checkIfExist.Description = course.Description;
						_db.SchCourses.Update(checkIfExist);
						_db.SaveChanges();

						return Json(new { isError = false, msg = "Created Successfully" });
					}
					else
					{
						return Json(new { isError = true, msg = "Course Already Exist" });
					}
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

		[HttpPost]
		public async Task<JsonResult> DeletCourse(int id)
		{
			if (id > 0)
			{
				var checkIfExist = _db.SchCourses.Where(a => a.Id == id).FirstOrDefault();
				if (checkIfExist != null)
				{
					checkIfExist.Deactivated = true;
					_db.SchCourses.Update(checkIfExist);
					_db.SaveChanges();
					return Json(new { isError = false, msg = "Deleted Successfully" });
				}
			}
			return Json(new { isError = true, msg = "Failed" });
		}

		[HttpGet]
		public IActionResult StudentCourseRegistraion()
		{
			var model = new List<StudentsCourses>();
			model = _userHelper.GetAllStudentCourseFromDB();
			return View(model);
		}
	}
}
