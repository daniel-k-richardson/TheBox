using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TheBox.Persistence.Users.DatabaseContext;

namespace TheBox.IntegrationTests;

public class WebApplicationFactorySetup : WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer postgreSqlContainer;

    public WebApplicationFactorySetup(PostgreSqlContainer postgreSqlContainer)
    {
        this.postgreSqlContainer = postgreSqlContainer;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType ==
                                                           typeof(DbContextOptions<UserDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Use the PostgreSQL container for the database
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseNpgsql(postgreSqlContainer.GetConnectionString());
            });
        });
    }
}
