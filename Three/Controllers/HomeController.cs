using Microsoft.AspNetCore.Mvc;
using Three.Services;

namespace Three.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IClock clock)
        {

        }
    }
}