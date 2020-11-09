using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var context = new ProductRouteContext
            {
                Country = "Germany",
                Category = "Smartphone",
                SearchQuery = "dual sim"
            };
            var url = Url.RouteUrl("ProductIndex", new { filters = context });
            return Content(url);
        }
    }
}