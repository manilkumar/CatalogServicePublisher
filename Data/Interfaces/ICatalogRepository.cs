
using CatalogService.API.Models;

namespace CatalogService.API.Data.Interfaces
{
    public interface ICatalogRepository
    {
        Task AddCatalog(Catalog category);
        Task AddItem(Item item);
        Task UpdateItem(Item item);
    }
}
