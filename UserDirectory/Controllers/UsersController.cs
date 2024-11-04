using Microsoft.AspNetCore.Mvc;
using UserDirectory.Services;

namespace UserDirectory.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var users = _userService.GetListOfUsers();
            return View(users);
        }

        // Action to display cities by country
        public ActionResult UsersByCountry(string countryName)
        {
            
        }

        // Action to view details of a specific city
        public ActionResult Details(int id)
        {

        }

    }
}
