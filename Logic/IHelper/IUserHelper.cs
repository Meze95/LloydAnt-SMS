using Core.Db;
using Core.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelper
{
    public interface IUserHelper
    {
		Task<ApplicationUser> FindByUserNameAsync(string userName);
		Task<ApplicationUser> FindByPhoneNumberAsync(string phoneNumber);
		Task<ApplicationUser> FindByEmailAsync(string email);
		Task<ApplicationUser> FindByIdAsync(string Id);
		List<ApplicationUserViewModel> GetAllStudentsFromDB();
		List<SchCourses> GetAllCourseFromDB();
		List<SchCourses> DropdownOfCourses(string userId);
		Task<bool> NewCourseRegistration(SchCourses data);
		Task<bool> NewStudentCourseRegistration(CourseResigtrationViewModel data);
		List<StudentsCourses> GetAllStudentCourseFromDB();
		List<StudentsCourses> GetStudentCourseByUserId(string userId);
	}
}
