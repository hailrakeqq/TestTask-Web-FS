using Microsoft.AspNetCore.Mvc;
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

    public Catalog[] GetParentCatalogs()
    {
        return _appDBContext.catalogs.Where(c => c.ParentId == null).ToArray();
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
