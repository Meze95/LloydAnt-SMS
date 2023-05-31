using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LloydAnt_SMS.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _db;
        private readonly IAccountsHelper _accountsHelper;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext db, IAccountsHelper accountsHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _accountsHelper = accountsHelper;
        }

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        //CREATE ADMIN ACCOUNT POST 
        [HttpPost]
        public async Task<JsonResult> CreateAdmin(string data)
        {
            try
            {
                var newUser = JsonConvert.DeserializeObject<ApplicationUserViewModel>(data);

                // Query the user details if it exists in the Db B4 Authentication
                var accountCheck = await _userManager.FindByEmailAsync(newUser.Email);
                if (accountCheck != null)
                {
                    return Json(new { isError = true, msg = "Email already exist" });
                }
                var result = _accountsHelper.CreateNewUser(newUser).Result;

                if (result != null)
                {
                    result.IsAdmin = true;
                    _db.Update(result);
                    _db.SaveChanges();

                    var addToRole = _userManager.AddToRoleAsync(result, "Admin").Result;
                    if (addToRole.Succeeded)
                    {
                        var url = "/Accounts/Login";
                        return Json(new { isError = false, msg = "Account Created Successfully", url = url });
                    }
                    return Json(new { isError = true, msg = "Failed" });

                }
                return Json(new { isError = true, msg = "Failed" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //CREATE ADMIN ACCOUNT POST 
        [HttpPost]
        public async Task<JsonResult> CreateUser(string data)
        {
            try
            {
                var newUser = JsonConvert.DeserializeObject<ApplicationUserViewModel>(data);

                // Query the user details if it exists in the Db B4 Authentication
                var accountCheck = await _userManager.FindByEmailAsync(newUser.Email);
                if (accountCheck != null)
                {
                    return Json(new { isError = true, msg = "Email already exist" });
                }
                var result = _accountsHelper.CreateNewUser(newUser).Result;

                if (result != null)
                {
                    var addToRole = _userManager.AddToRoleAsync(result, "User").Result;
                    if (addToRole.Succeeded)
                    {
                        var url = "/Accounts/Login";
                        return Json(new { isError = false, msg = "Account Created Successfully", url = url });
                    }
                    return Json(new { isError = true, msg = "Failed" });

                }
                return Json(new { isError = true, msg = "Failed" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login POST ACTION
        [HttpPost]
        public async Task<JsonResult> Login(string data)
        {
            if (data == null)
            {
                return Json(new { isError = true, msg = "Fill the form" });
            }

            var userDetails = JsonConvert.DeserializeObject<ApplicationUserViewModel>(data);
            if (userDetails.Email == "")
            {
                return Json(new { isError = true, msg = "Enter your email" });
            }
            if (userDetails.Password == "")
            {
                return Json(new { isError = true, msg = "Enter your password" });
            }
            var user = _db.ApplicationUser.Where(x => x.Email == userDetails.Email).FirstOrDefault();
            if (user != null)
            {
                var currentUser = await _signInManager.PasswordSignInAsync(user.UserName, userDetails.Password, true, true);
                if (currentUser.Succeeded)
                {
                    var dashboard = _accountsHelper.GetUserIndex(user);
                    return Json(new { isError = false, msg = "Welcome!", dashboard = dashboard });
                }
            }
            return Json(new { isError = true, msg = "Login Failed!!! Account not found" });
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
