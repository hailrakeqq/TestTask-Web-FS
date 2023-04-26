using Microsoft.AspNetCore.Mvc;

namespace TestTask.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
