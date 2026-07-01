using E_Commerce01.Domain.Contract;

namespace E_Commerce01.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedAndMigrationAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var catalogDataSeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await catalogDataSeeder.SeedDataAsync();

            return app;
        }
    }
}
