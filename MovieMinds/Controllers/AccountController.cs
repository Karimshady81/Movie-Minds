using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMinds.Data;
using MovieMinds.Models.DTO;
using MovieMinds.Models.Entites;
using MovieMinds.ViewModels;

namespace MovieMinds.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly MovieMindsDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, MovieMindsDbContext context, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = new LoginDto();
            return View(response);
        }

        public IActionResult Register()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = new RegisterDto();
            return View(response);
        }

        public async Task<IActionResult> Profile()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            // Get current user
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Load liked movies
            var likedMovies = await _context.UserMovies
                .Where(um => um.UserId == currentUser.Id && um.Liked)
                .Include(um => um.Movie)
                .Select(um => um.Movie)
                .Take(4)
                .ToListAsync();

            var inWatchList = await _context.UserMovies
                .Where(um => um.UserId == currentUser.Id && um.InWatchlist)
                .Include(um => um.Movie)
                .Select(um => um.Movie)
                .Take(4)
                .ToListAsync();

            var watchedMovies = await _context.UserMovies
                .Where(um => um.UserId == currentUser.Id && um.Watched)
                .Include(um => um.Movie)
                .Select(um => um.Movie)
                .Take(4)
                .ToListAsync();

            // Create and populate the ViewModel
            var response = new ProfilePageViewModel
            {
                CurrentUser = currentUser,
                LikedMovies = likedMovies,
                InWatchList = inWatchList,
                WatchedMovies = watchedMovies
            };

            return View(response);
        }

        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = new EditProfileViewModel
            {
                DisplayName = user.DisplayName ?? "",
                Bio = user.Bio,
                Location = user.Location,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            return View(response);
        }


        public async Task<IActionResult> AllMoviesAction(string type)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if(currentUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userMovies = _context.UserMovies
                    .Where(um => um.UserId == currentUser.Id)
                    .Include(um => um.Movie);

            var viewModel = new AllMoviesActionViewModel
            {
                CurrentUser = currentUser
            };

            switch (type?.ToLower())
            {
                case "liked":
                    viewModel.LikedMovies = await _context.UserMovies
                        .Where(um => um.UserId == currentUser.Id && um.Liked)
                        .Include(um => um.Movie)
                        .Select(um => um.Movie)
                        .ToListAsync();
                    return View("AllLiked", viewModel);
                   
                case "watchlist":
                    viewModel.InWatchList = await _context.UserMovies
                        .Where(um => um.UserId == currentUser.Id && um.InWatchlist)
                        .Include(um => um.Movie)
                        .Select(um => um.Movie)
                        .ToListAsync();
                    return View("AllWatchList", viewModel);

                case "watched":
                    viewModel.WatchedMovies = await _context.UserMovies
                        .Where(um => um.UserId == currentUser.Id && um.Watched)
                        .Include(um => um.Movie)
                        .Select(um => um.Movie)
                        .ToListAsync();
                    return View("AllWatched", viewModel);

                case "reviewed":
                    viewModel.Reviewed = await _context.UserMovies
                         .Where(um => um.UserId == currentUser.Id && um.Rating5 >= 1)
                         .Include(um => um.Movie)
                         .Select(um => um.Movie)
                         .ToListAsync();
                    return View("AllReviewed", viewModel);

                default:
                    return RedirectToAction("Profile", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.EmailOrUsername) ??
                       await _userManager.FindByNameAsync(model.EmailOrUsername);

            if (user != null)
            {
                //Checks the Users entered credentials
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(nameof(LoginDto.LockOut), "Account locked due to multiple failed attempts. Try again later.");
                    return View(model);
                }
                else
                {
                    //Password incorrect
                    ModelState.AddModelError(nameof(LoginDto.Password), "Invalid credentials. Please try again.");
                    return View(model);
                }
            }
            //User not found
            ModelState.AddModelError(nameof(LoginDto.EmailOrUsername), "Invalid credentials. Please try again.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

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
            else if(user?.UserName == model.UserName)
            {
                ModelState.AddModelError(nameof(RegisterDto.UserName), "User with this email or username already exists.");
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

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            user.DisplayName = model.DisplayName;
            user.Bio = model.Bio;
            user.Location = model.Location;
            user.ProfilePictureUrl = model.ProfilePictureUrl;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Profile", "Account");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
