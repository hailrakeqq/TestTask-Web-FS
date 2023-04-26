using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestTask.DTO;
using TestTask.Models;
using TestTask.Repository;

namespace TestTask.Services;

public class CatalogServices : ICatalogRepository
{
    private readonly AppDBContext _appDBContext;
    public CatalogServices(AppDBContext appDBContext)
    {
        _appDBContext = appDBContext;
    }

    public Catalog[] GetChildCatalogs(Catalog catalog)
    {
        if (catalog?.ChildDirectoriesId == null) return Enumerable.Empty<Catalog>().ToArray();

        return catalog.ChildDirectoriesId.Select(id => GetCatalogById(id)).ToArray();
    }

    public Catalog[] GetParentCatalogs()
    {
        return _appDBContext.catalogs.Where(c => c.ParentId == null).ToArray();
    }

    public string SerializeCatalog(Catalog catalog)
    {
        //TODO: complete serialize and return json for user
        string json = JsonConvert.SerializeObject(new
        {
            catalog.Id,
            catalog.Name,
            catalog.ParentId,
            ChildCatalogs = catalog.ChildDirectoriesId.Select(c => SerializeCatalog(GetCatalogById(c)))
        });
        return json;
    }

    public Catalog GetCatalogById(string id)
    {
        var catalog = _appDBContext.catalogs.Where(c => c.Id == id).FirstOrDefault();
        return catalog != null ? catalog : null;
    }

    public void CreateTest()
    {
        Catalog[] catalogs = new Catalog[] 
        {
            new Catalog
            {
                Id = "1", Name = "Creating Digital Images", ParentId = null, ChildDirectoriesId = new string[]{"1.1","1.2","1.3"}
            },
            new Catalog
            {
                Id = "1.1", Name = "Resources", ParentId = "1", ChildDirectoriesId = new string[] {"1.1.1","1.1.2"}
            },
            new Catalog
            {
                Id = "1.2", Name = "Evidence", ParentId = "1", ChildDirectoriesId = null
            },
            new Catalog
            {
                Id = "1.3", Name = "Graphic Products", ParentId = "1", ChildDirectoriesId = new string[]{"1.3.1", "1.3.2"}
            },
            new Catalog
            {
                Id = "1.1.1", Name = "Primary Sources", ParentId = "1.1", ChildDirectoriesId = null
            },
            new Catalog
            {
                Id = "1.1.2", Name = "Secondary Sources", ParentId = "1.1", ChildDirectoriesId = null
            },
            new Catalog
            {
                Id = "1.3.1", Name = "Process", ParentId = "1.3", ChildDirectoriesId = null
            },
            new Catalog
            {
                Id = "1.3.2", Name = "Final Product", ParentId = "1.3", ChildDirectoriesId = null
            },
        };
        foreach (var catalog in  catalogs)
        {
            _appDBContext.catalogs.Add(catalog);
        }

        _appDBContext.SaveChanges();
    }
    
    public List<Catalog> GetCatalogs()
    {
        throw new NotImplementedException();
    }
}
