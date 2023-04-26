namespace TestTask.Models;

public class Catalog
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? ParentId { get; set; } 
    public string[]? ChildDirectoriesId { get; set; }
}
public class CatalogForView
{
    public Catalog CurrentCatalog { get; set; }
    public Catalog? ParrentCatalog { get; set; }
    public Catalog[]? ChildDirectories { get; set; }
}
