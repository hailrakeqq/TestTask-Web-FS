using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Controllers;

public class FileController : Controller
{
    private readonly CatalogServices _catalogServices;
    private readonly FileServices _fileServices;
    public FileController(CatalogServices catalogServices, FileServices fileServices)
    {
        _catalogServices = catalogServices;
        _fileServices = fileServices;
    }
    
    public async Task<IActionResult> ExportFolderToJson(string parrentId)
    {
        var catalog = _catalogServices.GetCatalogById(parrentId);
        var json = _catalogServices.SerializeCatalog(catalog);
            
        string filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "ExportedCatalogs",
            $"{catalog.Name.Replace(" ", "")}.json");

        if (!System.IO.File.Exists(filePath))
            await _fileServices.CreateJsonFromString(catalog, json);

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var fileContent = await _fileServices.ReadStreamAsync(fileStream);
            
        return File(fileContent, "application/json", $"{catalog.Name.Replace(" ", "")}.json");
    }
    
    [HttpPost]
    public async Task<IActionResult> ImportCatalog(IFormFile file)
    {
        var result = await _fileServices.GetStringFromUploadedJson(file);
        if (result != null)
        {
            ViewBag.JsonData = result;
         
            var catalogsJson = await _catalogServices.CreateCatalogCollectionFromJson(result);
            var catalogs = _catalogServices.ConvertCatalogJSONToCatalogCollection(catalogsJson);
            await _catalogServices.AddCatalogsToDB(catalogs);

            return RedirectToAction("Index", "Catalog");
        }
        
        return BadRequest("Please select a valid JSON file to upload.");
    }
}
