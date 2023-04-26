using TestTask.Models;

namespace TestTask.Repository;

public interface ICatalogRepository
{
    List<Catalog> GetCatalogs();
    Catalog[] GetParentCatalogs();
    Catalog GetCatalogById(string id);
}
