using Microsoft.AspNetCore.Mvc;
using MovieMinds.Repositories.Interfaces;
using MovieMinds.ViewModels;
using System.Threading.Tasks;

namespace MovieMinds.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Details()
        {
            //ViewBag.Message = "User Details - To be implemented.";
            //return View(_userRepository.GetAllAsync());

            var users = await _userRepository.GetAllAsync();
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel(users, "User Details - To be implemented.");
            return View(userProfileViewModel);
        }
    }
}
