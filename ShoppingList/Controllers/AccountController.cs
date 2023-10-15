using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography.Xml;
using Shopping.ViewModels;
using Shopping.Security;
using Shopping.Models;

namespace Shopping.Controllers
{
    public class AccountController : Controller
    {
        ShoppingDbContext dbContext;
        public AccountController()
        {

            dbContext = new ShoppingDbContext();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                await SignIn(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("ACE", "Kullanıcı adı veya şifre hatalı.");
            }

            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                var check = await dbContext.Users.FirstOrDefaultAsync(u =>
                u.Email == newUser.Email);

                if (check == null)
                {
                    User user = new User()
                    {
                        Email = newUser.Email,
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Password = HashPassword(newUser.Password),
                        Role = "User"
                    };
                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();


                    LoginViewModel loginViewModel = new LoginViewModel()
                    {
                        Email = newUser.Email,
                        Password = newUser.Password
                    };
                    await SignIn(loginViewModel);
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ModelState.AddModelError("ACE", "Girdiğiniz bilgiler uygun değil!");
            }
            return View();
        }

        private string HashPassword(string password)
        {
            var SCollection = new ServiceCollection();
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<Hash>(LockerKey);
            string salt = locker.HashCreate();
            string encryptKey = locker.HashCreate(password, salt);

            return encryptKey;
        }

        private async Task<Task> SignIn(LoginViewModel user)
        {
            var check = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (check != null)
            {
                var SCollection = new ServiceCollection();
                SCollection.AddDataProtection();
                var LockerKey = SCollection.BuildServiceProvider();

                var locker = ActivatorUtilities.CreateInstance<Hash>(LockerKey);
                var encryptedKey = check.Password;

                string getEncryptKey = encryptedKey.Split('æ')[0];
                string getSalt = encryptedKey.Split('æ')[1];
                bool result = locker.ValidateHash(user.Password, getSalt, getEncryptKey);

                if (result)
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


                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
}
