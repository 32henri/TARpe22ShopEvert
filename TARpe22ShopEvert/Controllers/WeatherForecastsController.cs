using Microsoft.AspNetCore.Mvc;

namespace TARpe22ShopEvert.Controllers
{
    public class WeatherForecastsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
