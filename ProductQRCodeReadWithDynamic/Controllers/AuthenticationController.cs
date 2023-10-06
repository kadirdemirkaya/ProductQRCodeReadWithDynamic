using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Hubs;
using ProductQRCodeReadWithDynamic.Models.Authentication;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly INotificationReadRepository _notificationReadRepository;
        private readonly AppDbContext _context;
        public AuthenticationController(RoleManager<IdentityRole<int>> roleManager, UserManager<IdentityUser<int>> userManager, INotificationWriteRepository notificationWriteRepository, INotificationReadRepository notificationReadRepository, SignInManager<IdentityUser<int>> signInManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _notificationWriteRepository = notificationWriteRepository;
            _notificationReadRepository = notificationReadRepository;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                HttpContext.Session.SetString("CurrentUserName", user.UserName);
                HttpContext.Session.SetString("Email", user.Email);

                await _context.Set<Notification>().AddAsync(new() { Message = "LOGIN Succesfully", IsSuccess = true, Email = loginViewModel.Email, MessageType = "All", CreatedDate = DateTime.Now });
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }




        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerViewModel.UserName);
            if (userExists != null)
                return View();

            IdentityUser<int> iUser = new()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(iUser, registerViewModel.Password);
            var roleUser = new IdentityRole<int> { Name = "User" };
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(roleUser);
            await _userManager.AddToRoleAsync(iUser, "User");

            if (!result.Succeeded)
                return View();

            return RedirectToAction("Login", "Authentication");
        }


        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();

            await _context.Set<Notification>().AddAsync(new() { Message = "SIGNOUT Succesfully", IsSuccess = true, Email = HttpContext.Session.GetString("Email"), MessageType = "Personal", CreatedDate = DateTime.Now });
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Email");

            return RedirectToAction(nameof(Login));
        }

    }
}
