using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Core.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Address { get; set; }
		public bool IsAdmin { get; set; }
		public bool Deactivated { get; set; }
		public DateTime DateCreated { get; set; }
		public virtual ICollection<StudentsCourses> StudentsCourses { get; set; }
    }
}
