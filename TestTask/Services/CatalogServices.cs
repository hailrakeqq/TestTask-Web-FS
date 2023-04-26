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
    public Catalog GetCatalogById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Catalog> GetCatalogs()
    {
        throw new NotImplementedException();
    }
}
