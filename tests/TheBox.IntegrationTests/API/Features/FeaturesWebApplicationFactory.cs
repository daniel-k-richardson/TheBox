using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TheBox.Persistence.Users.DatabaseContext;

namespace TheBox.IntegrationTests.API.Features;

public class FeaturesWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private static readonly PostgreSqlContainer _postgreSqlContainer;

    static FeaturesWebApplicationFactory()
    {
        _postgreSqlContainer = CreatePostgreSqlContainer().Result;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
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

}