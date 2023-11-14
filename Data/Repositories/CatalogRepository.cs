using CatalogService.API.Data.Interfaces;
using CatalogService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.API.Data.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        public CatalogDBContext _context;

        public CatalogRepository(CatalogDBContext context)
        {
            _context = context;
        }

        public async Task AddCatalog(Catalog category)
        {
            await _context.Catalogs.AddAsync(category);
            await _context.SaveChangesAsync();
          
        }


        public async Task AddItem(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateItem(Item item)
        {
            var itemToUpdate = await _context.Items.Where(i => i.Id == item.Id).FirstOrDefaultAsync();

            if (itemToUpdate != null)
            {
                itemToUpdate.Name = item.Name;
                itemToUpdate.ImageFile = item.ImageFile;
                await _context.SaveChangesAsync();
            }
        }
    }
}
