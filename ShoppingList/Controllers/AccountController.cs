using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;
using ShoppingList.ViewModels;
using System.Security.Claims;
using System.Text;

namespace ShoppingList.Controllers
{
    public class AccountController : Controller
    {
        ShoppingDbContext dbContext;
        public AccountController() { 
        
            dbContext = new ShoppingDbContext();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel user) {

            if(ModelState.IsValid)
            {   
                var check = await dbContext.Users.FirstOrDefaultAsync(u =>
                u.Email == user.Email && u.Password == user.Password);
                if (check != null)
                {
                    string role = check.Role;
                    int id = check.UserId;

                    List<Claim> claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));

                    var claimsIdentify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentify), authProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("ACE", "Kullanıcı adı veya şifre hatalı.");
                }
            } else
            {
                ModelState.AddModelError("MSE","Kullanıcı adı veya şifre hatalı.");
            }

            return View();
        }

        /*
        private string HashPassword(string password)
        {

            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
        */
        
    }
}
