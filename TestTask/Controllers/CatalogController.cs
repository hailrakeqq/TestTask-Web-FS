using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogServices _catalogServices;
        public CatalogController(CatalogServices catalogServices)
        {
            _catalogServices = catalogServices;
        }
        public IActionResult Index()
        {
            var mainCatalog = _catalogServices.GetParentCatalogs().FirstOrDefault();
            var mainCatalogChild = new Catalog[mainCatalog.ChildDirectoriesId.Length];
              
            for (int i = 0; i < mainCatalogChild.Length; i++)
                mainCatalogChild[i] = _catalogServices.GetCatalogById(mainCatalog.ChildDirectoriesId[i]);
                
            return View(new CatalogForView { CurrentCatalog = mainCatalog, ChildDirectories = mainCatalogChild});
        }
        
        public IActionResult GetCatalogById(string id)
        {
            var mainCatalog = _catalogServices.GetCatalogById(id);
            Catalog[] mainCatalogChild = null;
            if (mainCatalog.ChildDirectoriesId != null)
            {
                mainCatalogChild = new Catalog[mainCatalog.ChildDirectoriesId.Length];
                
                for (int i = 0; i < mainCatalogChild.Length; i++)
                    mainCatalogChild[i] = _catalogServices.GetCatalogById(mainCatalog.ChildDirectoriesId[i]);
            }
            
            var thisParrentCatalog = _catalogServices.GetCatalogById(mainCatalog.ParentId);

            return View(new CatalogForView
            {
                CurrentCatalog = mainCatalog,
                ParrentCatalog = thisParrentCatalog,
                ChildDirectories = mainCatalogChild
            });
        }

        public IActionResult CreateTest()
        {
            _catalogServices.CreateTest();
            return Ok("Catalog was added please check DB.");
        }
    }
}
