using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class AccountsHelper: IAccountsHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _db;

        public AccountsHelper(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string GetUserIndex(ApplicationUser userr)
        {
            var userRole = _userManager.GetRolesAsync(userr).Result;
            if (userRole.Contains("Admin"))
            {
                 return "/Admin/Index";
            }
            else
            {
                return "/Student/Index";
            }
            return null;
        }

        public async Task<ApplicationUser> CreateNewUser(ApplicationUserViewModel applicationUserViewModel)
        {
            if (applicationUserViewModel != null)
            {
                var newUser = new ApplicationUser();
                {
                    newUser.FirstName = applicationUserViewModel.FirstName;
                    newUser.LastName = applicationUserViewModel.LastName;
                    newUser.Email = applicationUserViewModel.Email;
                    newUser.UserName = applicationUserViewModel.Email;
                    newUser.PhoneNumber = applicationUserViewModel.PhoneNumber;
                    newUser.Address = applicationUserViewModel.Address;
                    newUser.Deactivated = false;
                    newUser.DateCreated = DateTime.Now.Date;
                }
                var result = await _userManager.CreateAsync(newUser, applicationUserViewModel.Password);
                if (result.Succeeded)
                {
                    return newUser;
                }
                return null;
            }
            return null;

        }
    }
}
