using System.Threading.Tasks;
using HomeWork.Models;
using HomeWork.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public Account(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            };
            var existUser = await _userManager.FindByNameAsync(registerViewModel.Username);
            if (existUser !=null)
            {
                ModelState.AddModelError("Username","Bu adda User movcuddur");
                return View();
            }

            var user = new User
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username,
                Fullname = registerViewModel.Fullname
            };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                    return View();
                }
            }

            await _signInManager.SignInAsync(user,false);
            return RedirectToAction("Index", "Home");
        }
        
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await _userManager.FindByNameAsync(loginViewModel.Username);
            if (existUser==null)
            {
                ModelState.AddModelError("","Invalid Credentials");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(existUser, loginViewModel.Password, loginViewModel.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("","Invalid Credentials");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Index", "Home");
        }
    }
}