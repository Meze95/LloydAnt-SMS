using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class UserHelper : IUserHelper
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AppDbContext _db;

		public UserHelper(AppDbContext db, UserManager<ApplicationUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}

		public async Task<ApplicationUser> FindByUserNameAsync(string userName)
		{
			return _userManager.Users.Where(u => u.UserName == userName)?.Include(c => c.StudentsCourses).FirstOrDefaultAsync().Result;
		}
		public async Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber)
		{
			return await _userManager.Users.Where(s => s.PhoneNumber == phoneNumber)?.Include(c => c.StudentsCourses).FirstOrDefaultAsync();
		}
		public async Task<ApplicationUser> FindByEmailAsync(string email)
		{
			return await _userManager.Users.Where(s => s.Email == email)?.Include(c => c.StudentsCourses).FirstOrDefaultAsync();
		}
		public async Task<ApplicationUser> FindByIdAsync(string Id)
		{
			return await _userManager.Users.Where(s => s.Id == Id)?.Include(c => c.StudentsCourses).FirstOrDefaultAsync();
		}

		public List<ApplicationUserViewModel> GetAllStudentsFromDB()
		{
			var students = new List<ApplicationUserViewModel>();
			var allstudents = _db.ApplicationUser.Where(b => b.Deactivated == false && b.IsAdmin == false).OrderByDescending(d => d.DateCreated).ToList();
			if (allstudents.Any())
			{
				students = allstudents.Select(s => new ApplicationUserViewModel()
				{
					Id = s.Id,
					Email = s.Email,
					FirstName = s.FirstName,
					LastName = s.LastName,
					PhoneNumber = s.PhoneNumber,
					DateCreated = s.DateCreated
				}).ToList();
				return students;
			}
			return students;
		}

		public List<SchCourses> GetAllCourseFromDB()
		{
			var courses = new List<SchCourses>();
			var allcourses = _db.SchCourses.Where(c => c.Id > 0).ToList();
			if (allcourses.Any())
			{
				courses = allcourses;
			}
			return courses;
		}

		public async Task<bool> CreateNewCourse(SchCourses data)
		{
			if (data != null)
			{
				if (data.Name == null && data.Description != null)
				{
					var result = await _db.SchCourses.AddAsync(data).ConfigureAwait(false);
					_db.SaveChanges();
					if (result != null)
					{
						return true;
					}

					return false;
				}
			}
			return false;
		}

        public List<SchCourses> DropdownOfCourses()
        {
            try
            {
                var common = new SchCourses()
                {
                    Id = 0,
                    Name = "Select Courses"
                };
                var courses = _db.SchCourses.Where(x => x.Id != 0 && !x.Deactivated).OrderBy(p => p.Name).ToList();
                courses.Insert(0, common);
                return courses;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

		public async Task<bool> NewCourseRegistration(SchCourses data)
		{
			if (data != null)
			{
				if (data.Name != null && data.Description != null)
				{
					data.DateCreated = DateTime.Now;
					var result = await _db.SchCourses.AddAsync(data).ConfigureAwait(false);
					_db.SaveChanges();
					if (result != null)
					{
						return true;
					}

					return false;
				}
			}
			return false;
		}

        public async Task<bool> NewStudentCourseRegistration(CourseResigtrationViewModel data)
        {
			var listOfCourses = new List<StudentsCourses>();
            if (data.UserId != null && data.CourseIds.Any())
            {
				foreach (var c in data.CourseIds)
				{
					int id = Convert.ToInt32(c);
					var checkIfRegistered = _db.StudentsCourses.Where(c => c.UserId == data.UserId && c.CourseId == id).FirstOrDefault();
					if (checkIfRegistered == null) {
                        var Course = new StudentsCourses()
						{
							CourseId = id,
							UserId = data.UserId,
							DateCreated = DateTime.Now,
							Deactivated = false,
						};
						listOfCourses.Add(Course);
                    }
				}
                if (listOfCourses.Any())
                {
                    await _db.StudentsCourses.AddRangeAsync(listOfCourses).ConfigureAwait(false);
                    _db.SaveChanges();
                }
				return true;
			}
			return false;
        }
    }
}
