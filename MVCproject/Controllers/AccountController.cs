using Microsoft.AspNetCore.Authentication;
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
        public IActionResult Register()
        {

            return View();
        }
        //[HttpPost]
        //public IActionResult Register()
        //{
        //    return View();
        //}
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

                ClaimsIdentity d1 = new ClaimsIdentity("Cookies");
                d1.AddClaim(c1);
                d1.AddClaim(c2);

                ClaimsPrincipal cp = new ClaimsPrincipal();
                cp.AddIdentity(d1);

                await HttpContext.SignInAsync(cp);
                return RedirectToAction("Index", "Home");

            }

            return View();
        }
    }
}
