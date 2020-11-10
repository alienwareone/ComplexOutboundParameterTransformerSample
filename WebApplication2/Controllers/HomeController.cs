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
            // url == "/Germany/Smartphone%3Fq%3Ddual%20sim"
            // Should be: "/Germany/Smartphone?q=dual%20sim"

            var url2 = Url.RouteUrl("ProductIndex", new { filters = context, x = 1, y = 2, z = 3 });
            // url2 == "/Germany/Smartphone%3Fq%3Ddual%20sim?x=1&y=2&z=3"
            // This should be merged to: "/Germany/Smartphone?q=dual%20sim&x=1&y=2&z=3"

            return Content(url);
        }
    }
}