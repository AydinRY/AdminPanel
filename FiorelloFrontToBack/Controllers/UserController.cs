using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeWork.DataAccessLayer;
using HomeWork.Models;
using HomeWork.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers
{
    [Area("AdminPanel")]
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    Fullname = user.Fullname,
                    Username = user.UserName,
                    Email = user.Email,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()??"Doesnt exist role",
                    IsActive = user.IsActive
                };
                userViewModels.Add(userViewModel);
            }
            return View(userViewModels);
        }

        public async Task<IActionResult> Activate(string id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var existUser = await _dbContext.Users.FindAsync(id);
            if (existUser==null)
            {
                return NotFound();
            }

            
            if (existUser.IsActive==true)
            {
                existUser.IsActive = false;
            }
            else
            {
                existUser.IsActive = true;
            }

            await _userManager.UpdateAsync(existUser);
            return RedirectToAction(nameof(Index));

        }
        
        public async Task<IActionResult> ChangeRole(string id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (role==null)
            {
                return BadRequest();
            }
            ViewBag.CurrentRole = role;

            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ChangeRole(string id,string NewRole)
        {
            if (id==null && NewRole == null)
            {
                return  NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user==null)
            {
                return NotFound();
            }
            string existRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (existRole == null)
            {
                return NotFound();
            }
            if (existRole!= NewRole)
            {
                var result= await _userManager.AddToRoleAsync(user, NewRole);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "This is Problem");
                }
                var DeleteRole = await _userManager.RemoveFromRoleAsync(user, existRole);
                if (DeleteRole.Succeeded)
                {
                    ModelState.AddModelError("", "This is problem");
                }
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult ChangePassword(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return BadRequest();
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (checkPassword==false)
            {
                ModelState.AddModelError("CurrentPassword", "Input Password Correctly");
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
           
            return RedirectToAction(nameof(Index));
        }
    }
}