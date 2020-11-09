using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace WebApplication2.Controllers
{
    [Route("{**filters:productFilters(search)}", Name = "ProductIndex")]
    public class ProductsController : Controller
    {
        public IActionResult Index(string filters)
        {
            return Content(filters);
        }
    }
}