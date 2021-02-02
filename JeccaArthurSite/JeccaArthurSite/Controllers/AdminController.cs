using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Winterfell.Models;
using System.Linq;
using Winterfell.Repositories;

namespace Winterfell.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IMessages messageRepo;

        public AdminController(UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr,
            IMessages msgRepo)
        {
            userManager = userMngr;
            roleManager = roleMngr;
            messageRepo = msgRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>();
            foreach (AppUser user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }

            AdminVM model = new AdminVM
            {
                Users = users,
                Roles = roleManager.Roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityResult result = null;
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                // check to see if the user has sent or received a message
                if (0 == (from m in messageRepo.Messages
                          where m.Sender.Name == user.Name || m.Recipient.Name == user.Name
                          select m).Count<Message>())
                {

                    result = await userManager.DeleteAsync(user);
                }
                else
                {
                    result = IdentityResult.Failed(new IdentityError()
                    { Description = "User's messages must be deleted first" });
                }

                if (!result.Succeeded)
                {
                    // if failed 
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += errorMessage != "" ? " | " : "";   // put a separator between messages
                        errorMessage += error.Description;
                    }
                    TempData["message"] = errorMessage;
                }
                else
                {
                    TempData["message"] = "";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");

            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. "
                    + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            var result = await userManager.RemoveFromRoleAsync(user, "Admin");

            if (result.Succeeded) { }

            return RedirectToAction("Index");
        }


        // role management methods

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));

            return RedirectToAction("Index");
        }
    }
}
