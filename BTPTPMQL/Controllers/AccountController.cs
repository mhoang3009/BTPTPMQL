using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTPTPMQL.Models;
using BTPTPMQL.Models.ViewModels;
using System.Security.Claims;
using BTPTPMQL.Models.Process;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace BTPTPMQL.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userWithRoles = new List<UserWithRoleVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userWithRoles.Add(new UserWithRoleVM { User = user, Roles = roles.ToList() });

            }
            return View(userWithRoles);
        }
        public async Task<IActionResult> AssignRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles != null
                ? await _roleManager.Roles.Select(r => new RoleVM { Id = r.Id, Name = r.Name }).ToListAsync()
                : new List<RoleVM>();
            var ViewModels = new AssignRoleVM
            {
                UserId = userId,
                AllRoles = allRoles,
                SelectedRoles = userRoles
            };
            return View(ViewModels);
        }

        public async Task<IActionResult> AddClaim(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userClaims = await _userManager.GetClaimsAsync(user);

            var model = new UserClaimVM(userId, user.UserName, userClaims.ToList());

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddClaim(string userId, string claimType, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
            if (result.Succeeded)
            {
                return RedirectToAction("AddClaim", new { userId });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.UserId))
                {
                    return BadRequest("User ID cannot be null or empty.");
                }

                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                if (model.SelectedRoles != null)
                {
                    foreach (var role in model.SelectedRoles)
                    {
                        if (!userRoles.Contains(role))
                        {
                            await _userManager.AddToRoleAsync(user, role);
                        }
                    }
                }

                foreach (var role in userRoles)
                {
                    if (model.SelectedRoles == null || !model.SelectedRoles.Contains(role))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                }

                return RedirectToAction("Index", "Account");
            }

            return View(model);
        }
    }

}
