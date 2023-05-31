using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Db
{
	public class AppDbContext: IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<ApplicationUser> ApplicationUser { get; set; }
		public DbSet<SchCourses> SchCourses { get; set; }
		public DbSet<StudentsCourses> StudentsCourses { get; set; }
	}
}
