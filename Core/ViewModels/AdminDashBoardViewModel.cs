using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class AdminDashBoardViewModel
    {
        public virtual ApplicationUser Users { get; set; }
        public int? TotalActiveStudents { get; set; }
        public virtual List<ApplicationUserViewModel> Students { get; set; }
    }
}
