using E_Commerce01.Domain.Contract;
using E_Commerce01.Domain.Entities.Base;
using E_Commerce01.Domain.Entities.Product;
using E_Commerce01.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce01.Infrastructure.DataSeeding
{
    public class CatalogDataSeeder(StoreDbContext context , ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigrations = await context.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigrations.Any())
                {
                    await context.Database.MigrateAsync(ct);
                }

                var root = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedEmptyData<ProductBrand, int>(root, "brands.json" , ct);
                await SeedEmptyData<ProductType, int>(root, "types.json" , ct);
                await SeedEmptyData<Product, int>(root, "products.json" , ct);

                var count = await context.SaveChangesAsync(ct);

                if (count > 0)
                    logger.LogWarning($"{count} Rows Added");
                else
                {
                    logger.LogWarning($"There is no rows affected");
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }


        private async Task SeedEmptyData<TEntity , TKey>(string rootPath ,string fileName , CancellationToken ct = default) where TEntity : BaseEntity<TKey>
        {
            if(await context.Set<TEntity>().AnyAsync(ct))
            {
                logger.LogWarning("Table already has data");
                return;
            }

            var filePath = Path.Combine(rootPath , fileName);
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"File {fileName} not exists");
                return;
            }

            using var fileStream = File.OpenRead(filePath);

            // Seed
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var data = await JsonSerializer.DeserializeAsync<List<TEntity>>(fileStream , options);

            if(data is not null && data.Any())
                await context.Set<TEntity>().AddRangeAsync(data , ct);

        }
    }
}
