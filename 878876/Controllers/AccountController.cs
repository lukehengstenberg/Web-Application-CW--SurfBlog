using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _878876.Models;
using Microsoft.AspNetCore.Identity;
using _878876.ViewModels;
using Microsoft.AspNetCore.Authorization;
using _878876.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace _878876.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Policy ="AddEditUser")]
        [HttpGet]
        public IActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            model = _userManager.Users.Select(u => new UserListViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
            return View(model);
        }

        [Authorize(Policy = "AddEditUser")]
        [HttpGet]
        public IActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();
            model.UserClaims = ClaimData.UserClaims.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }).ToList();
            return View("AddUser", model);
        }

        [Authorize(Policy ="AddEditUser")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddUser([Bind("Id,Name,Email,UserClaims,UserName,Password")]UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                List<SelectListItem> userClaims = model.UserClaims.Where(c => c.Selected).ToList();
                foreach(var claim in userClaims)
                {
                    await _userManager.AddClaimAsync(user, new Claim(claim.Value, claim.Value));
                }

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [Authorize(Policy = "AddEditUser")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();

            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                    model.Name = user.Name;
                    model.Email = user.Email;
                    var claims = await _userManager.GetClaimsAsync(user);

                    model.UserClaims = ClaimData.UserClaims.Select(c => new SelectListItem
                    {
                        Text = c,
                        Value = c,
                        Selected = claims.Any(x => x.Value == c)
                    }).ToList();
                }
                else
                {
                    model.UserClaims = ClaimData.UserClaims.Select(c => new SelectListItem
                    {
                        Text = c,
                        Value = c
                    }).ToList();
                }
            }
            return View("EditUser", model);
        }

        [Authorize(Policy = "AddEditUser")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditUser(string id, [Bind("Id,Name,Email,UserClaims")] EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Email = model.Email;
                    var claims = await _userManager.GetClaimsAsync(user);

                    List<SelectListItem> userClaims = model.UserClaims.Where(c => c.Selected && claims.Any(u => u.Value != c.Value)).ToList();
                    foreach (var claim in userClaims)
                    {
                        await _userManager.AddClaimAsync(user, new Claim(claim.Value, claim.Value));
                    }
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    List<Claim> userRemoveClaims = claims.Where(c => model.UserClaims.Any(u => u.Value == c.Value && !u.Selected)).ToList();
                    foreach (Claim claim in userRemoveClaims)
                    {
                        await _userManager.RemoveClaimAsync(user, claim);
                    }
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("EditUser", model);
        }

        [Authorize(Policy = "DeleteUser")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    name = user.Name;
                }
            }
            return View("DeleteUser", name);
        }

        [Authorize(Policy = "DeleteUser")]
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser
            {
                UserName = vm.Email,
                Email = vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Posts");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(vm);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password,
                vm.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Posts");
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}