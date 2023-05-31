using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class CourseResigtrationViewModel
	{
		public int Id { get; set; }
		public bool Deactivated { get; set; }
		public DateTime DateCreated { get; set; }
		public int CourseId { get; set; }
		public virtual SchCourses Course { get; set; }
		public List<string> CourseIds { get; set; }
		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}
