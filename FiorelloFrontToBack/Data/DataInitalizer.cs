using System.Collections.Generic;
using System.Threading.Tasks;
using HomeWork.DataAccessLayer;
using HomeWork.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Data
{
    public class DataInitalizer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DataInitalizer(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task SeedDataAsync()
        {
            await _dbContext.Database.MigrateAsync();
            var roles = new List<string>
            {
                RoleConstants.AdminRole,
                RoleConstants.ModeratorRole,
                RoleConstants.UserRole
            };
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    continue;

                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = new User
            {
                Fullname = "Admin Admin",
                UserName = "Admin",
                Email = "admin@code.az"
            };
            if (await _userManager.FindByNameAsync(user.Fullname) != null)
                return;

            await _userManager.CreateAsync(user, "Admin@1234");
            await _userManager.AddToRoleAsync(user, RoleConstants.AdminRole);
        }
    }
}