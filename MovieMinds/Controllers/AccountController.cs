using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;

namespace MovieMinds.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            var response = new LoginDto();
            return View(response);
        }

        public IActionResult Register()
        {
            var response = new RegisterDto();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.EmailOrUsername) ??
                       await _userManager.FindByNameAsync(model.EmailOrUsername);

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: default);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    //Password incorrect
                    ModelState.AddModelError("Password", "Invalid credentials. Please try again.");
                    return View(model);
                }
                return View(model);
            }
            //User not found
            ModelState.AddModelError(string.Empty, "Invalid credentials. Please try again.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email) ??
                       await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                ModelState.AddModelError(nameof(RegisterDto.Email), "User with this email or username already exists.");
                return View(model);
            }

            var newUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                DisplayName = model.DisplayName
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
