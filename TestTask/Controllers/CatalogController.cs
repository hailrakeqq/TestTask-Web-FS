using Microsoft.AspNetCore.Mvc;
using TestTask.DTO;
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
            var catalogDTOFromToSend = new List<CatalogDTO>();
            var mainCatalogs = _catalogServices.GetParentCatalogs();
            
            foreach (var catalog in mainCatalogs)
            {
                catalogDTOFromToSend.Add(
                    new CatalogDTO{CurrentCatalog = catalog,
                    ChildCatalogs = _catalogServices.GetChildCatalogs(catalog)}
                    );    
            }
            
            return View(catalogDTOFromToSend);
        }
        
        public IActionResult GetCatalogById(string id)
        {
            var mainCatalog = _catalogServices.GetCatalogById(id);

            return View(new CatalogDTO()
            {
                CurrentCatalog = mainCatalog, 
                ChildCatalogs = _catalogServices.GetChildCatalogs(mainCatalog),
                ParrentCatalog = _catalogServices.GetCatalogById(mainCatalog.ParentId)
            });
        }

        public IActionResult ExportFolderToJson(string parrentId)
        {
            return Ok(_catalogServices.SerializeCatalog(_catalogServices.GetCatalogById(parrentId)));
        }

        public IActionResult CreateTest()
        {
            _catalogServices.CreateTest();
            return Ok("Catalog was added please check DB.");
        }
    }
}
