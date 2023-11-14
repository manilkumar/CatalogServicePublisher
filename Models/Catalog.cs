namespace CatalogService.API.Models
{
    public class Catalog
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Item Item { get; set; }
        
    }
}
