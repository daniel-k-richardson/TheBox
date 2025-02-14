using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TheBox.Persistence.Users.DatabaseContext;
using Xunit;

namespace TheBox.IntegrationTests.API.Features;

public class FeaturesWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("TestDb")
        .WithUsername("postgres")
        .WithPassword("Strong_password_123!")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<UserDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Use the PostgreSQL container for the database
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });

            // Seed the database with test data
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            db.Database.EnsureCreated();
        });
    }

    private static async Task<PostgreSqlContainer> CreatePostgreSqlContainer()
    {
        var container = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("TestDb")
            .WithUsername("postgres")
            .WithPassword("Strong_password_123!")
            .Build();

        await container.StartAsync();
        return container;
    }

    public Task InitializeAsync()
    {
        return this._postgreSqlContainer.StartAsync();
    }
    public new Task DisposeAsync()
    {
        return this._postgreSqlContainer.StopAsync();
    }
}