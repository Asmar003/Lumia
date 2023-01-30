using Bilet.Models;
using Bilet.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bilet.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = new AppUser
            {
                Name = userRegisterVM.Name,
                Surname = userRegisterVM.Surname,
                UserName = userRegisterVM.Username,
                Email = userRegisterVM.Email
            };
            var result=await _userManager.CreateAsync(appUser, userRegisterVM.Password);
            if(!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(appUser,true);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user =await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                if(user == null )
                {
                    ModelState.AddModelError("", "Login or password wrong");
                return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsPersistance, true);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Login or password wrong");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
