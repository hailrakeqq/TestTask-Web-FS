using TestTask.Models;

namespace TestTask.DTO;

public class CatalogDTO
{
    public Catalog CurrentCatalog { get; set; }
    public Catalog? ParrentCatalog { get; set; }
    public Catalog[]? ChildCatalogs { get; set; }
}