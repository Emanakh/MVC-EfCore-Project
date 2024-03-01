using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MVCproject.Models;
using MVCproject.Repository;
using MVCproject.ViewModels;
using System.Security.Claims;

namespace MVCproject.Controllers
{
    public class AccountController : Controller
    {
        IAccount Account;
        public AccountController(IAccount _Account)
        {
            Account = _Account;
        }
        public IActionResult AccessDenied()
        {
            ViewBag.Message = "Access Denied. You do not have permission to view this page.";
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {

                User NewUser = new User() { Name = user.Name, Email = user.Email, Age = user.Age, Password = user.Password };
                Role role = Account.FindRole("Student");
                NewUser.Roles.Add(role);
                Account.AddNewUser(NewUser);
                return RedirectToAction("Login");
            }
            return View(user);
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            User Found = Account.FindUser(user);
            if (Found == null)
            {
                ModelState.AddModelError("", "user name or password or both are invalid");
                return View(Found);
            }
            else if (Found != null)
            {
                //claims
                Claim c1 = new Claim(ClaimTypes.Name, Found.Name);
                Claim c2 = new Claim(ClaimTypes.Email, Found.Email);
                List<Claim> Claims = new List<Claim>();

                foreach (Role role in Found.Roles)
                {
                    Claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }


                ClaimsIdentity d1 = new ClaimsIdentity("Cookies");
                d1.AddClaim(c1);
                d1.AddClaim(c2);
                foreach (Claim c in Claims)
                {
                    d1.AddClaim(c);
                }



                ClaimsPrincipal cp = new ClaimsPrincipal();
                cp.AddIdentity(d1);

                await HttpContext.SignInAsync(cp);
                return RedirectToAction("Index", "Home");

            }

            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");


        }

    }
}
