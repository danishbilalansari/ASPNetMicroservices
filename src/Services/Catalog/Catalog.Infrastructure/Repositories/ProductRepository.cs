using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        private readonly ICatalogContext _context;
        
        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            // Find in following linq is part of IMongoCollection
            return await _context
                        .Products
                        .Find(x => true)
                        .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            // Find in following linq is part of IMongoCollection
            return await _context
                        .Products
                        .Find(x => x.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrands(string brandName)
        {
            // FilterDefinition is a part of MongoDB.Driver and used for filtering specific entity
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Brands.Name, brandName);

            // Find in following linq is part of IMongoCollection
            return await _context
                        .Products
                        .Find(filter)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            // FilterDefinition is a part of MongoDB.Driver and used for filtering specific entity
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            
            return await _context
                        .Products
                        .Find(filter)
                        .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            // ReplaceOneAsync is part of IMongoCollection
            var updatedResult = await _context
                                    .Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            // FilterDefinition is a part of MongoDB.Driver and used for filtering specific entity
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            // DeleteResult is part of MongoDb.Driver and DeleteOneAsynce is part of IMongoCollection
            DeleteResult deleteResult = await _context
                                            .Products
                                            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context
                .Brands
                .Find(brand => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context
                .Types
                .Find(type => true)
                .ToListAsync();
        }
    }
}
