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
        string json = JsonConvert.SerializeObject(new
        {
            catalog.Id,
            catalog.Name,
            catalog.ParentId,
            ChildCatalogs =  catalog.ChildDirectoriesId == null ? 
                null : catalog.ChildDirectoriesId.Select(c => SerializeCatalog(GetCatalogById(c)))
        });
        return json.Replace("\\", "")
                   .Replace("\"{", "{")
                   .Replace("}\"", "}");
    }

    public Catalog GetCatalogById(string id)
    {
        var catalog = _appDBContext.catalogs.Where(c => c.Id == id).FirstOrDefault();
        return catalog != null ? catalog : null;
    }

    public async Task<bool> CreateJsonFromString(Catalog catalog,string json)
    {
        string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "ExportedCatalogs", $"{catalog.Name.Replace(" ", "")}.json");
        if (!System.IO.File.Exists(pathToFile))
        {
            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                writer.Write(json);
            }
            return true;
        }

        return false;
    }
    
    public async Task<List<CatalogJson>> CreateCatalogCollectionFromJson(string json)
    {
        var catalog = JsonConvert.DeserializeObject<CatalogJson>(json);
        var catalogs = new List<CatalogJson>();
        FindCatalogsInJsonDocument(catalog, catalogs);
        return catalogs;
    }

    private void FindCatalogsInJsonDocument(CatalogJson catalog, List<CatalogJson> catalogs)
    {
        catalogs.Add(catalog);
        
        if (catalog.ChildCatalogs != null)
        {
            foreach (var chieldCatalog in catalog.ChildCatalogs)
            {
                FindCatalogsInJsonDocument(chieldCatalog, catalogs);
            }    
        }
    }

    public List<Catalog> ConvertCatalogJSONToCatalogCollection(List<CatalogJson> catalogsJson)
    {
        var catalogs = new List<Catalog>(catalogsJson.Count);
        foreach (var catalogJson in catalogsJson)
        {
            catalogs.Add(new Catalog()
            {
                Id = catalogJson.Id,
                Name = catalogJson.Name,
                ParentId = catalogJson.ParentId,
                ChildDirectoriesId = catalogJson.ChildCatalogs == null ? 
                    null : catalogJson.ChildCatalogs.Select(child => child.Id).ToArray()
            });
        }

        return catalogs;
    }
    
    public async Task<bool> AddCatalogToDB(Catalog catalog)
    {
        try
        {
            var existedCatalog = GetCatalogById(catalog.Id);
            if (existedCatalog != null)
                return false;
            
            await _appDBContext.catalogs.AddAsync(catalog);
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public async Task<bool> AddCatalogsToDB(List<Catalog> catalogs)
    {
        try
        {
            foreach (var catalog in catalogs)
            {
                var existedCatalog = GetCatalogById(catalog.Id);
                if (existedCatalog != null)
                    continue;
                await _appDBContext.catalogs.AddAsync(catalog);
            }
            
            await _appDBContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task CreateInitialCatalogs()
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

        await AddCatalogsToDB(catalogs.ToList());

        await _appDBContext.SaveChangesAsync();
    }
}
