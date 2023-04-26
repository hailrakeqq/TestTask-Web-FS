using TestTask.Models;

namespace TestTask.Repository;

public interface ICatalogRepository
{
    Catalog[] GetChildCatalogs(Catalog parrentCatalogId);
    Catalog[] GetParentCatalogs();
    string SerializeCatalog(Catalog catalog);
    Catalog GetCatalogById(string id);
}
