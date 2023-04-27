using Microsoft.AspNetCore.Mvc;
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
    public ActionResult ImportCatalog([FromBody] Catalog catalog)
    {
        //TODO: Create import system
        return Ok(catalog);
    }
}