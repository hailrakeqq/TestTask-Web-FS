namespace TestTask.Models;

public class Catalog
{
    public string Id { get; set; }
    public string Name { get; set; }    
    public Catalog[] subCatalogs { get; set; }
}
