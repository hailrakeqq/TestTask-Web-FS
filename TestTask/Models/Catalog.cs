namespace TestTask.Models;

public class Catalog
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? ParentId { get; set; } 
    public string[]? ChildDirectoriesId { get; set; }
}

public class CatalogJson
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? ParentId { get; set; } 
    public List<CatalogJson>? ChildCatalogs { get; set; }
}