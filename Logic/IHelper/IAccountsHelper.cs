using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelper
{
    public interface IAccountsHelper
    {
        string GetUserIndex(ApplicationUser userr);
        Task<ApplicationUser> CreateNewUser(ApplicationUserViewModel applicationUserViewModel);
    }
}
